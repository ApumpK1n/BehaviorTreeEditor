using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using System;
using UnityEditor;
using UnityEditor.UIElements;
using System.Reflection;

namespace Pumpkin.AI.BehaviorTree
{
    public enum BTPortCapacity
    {
        None,
        Single,
        Multi
    }

    public class BTGraphNode<T> : Node, ISavable where T : SerializableProperty, new()
    {
        protected static Vector2 DefaultNodeSize = new Vector2(300f, 400f);
        protected NodeProperty m_NodeProperty;

        private VisualElement m_PropertyContainer;
        private Func<object> ActionNodePropetyConstructFunc;

        protected string m_Guid;

        protected DataManager m_DataManger;

        private T m_PropertyData;
        private GraphSerializableNodeData m_GraphSerializableNodeData;
        public string Guid => m_Guid;

        public BTGraphNode(Vector2 pos, NodeProperty nodeProperty, GraphSerializableNodeData graphSerializableNodeData=null)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/BTGraphNode"));
            AddToClassList("bold-text");

            m_NodeProperty = nodeProperty;
            m_GraphSerializableNodeData = graphSerializableNodeData;

            if (graphSerializableNodeData != null) UnpackPropertyJson();
            else m_PropertyData = new T();

            if (graphSerializableNodeData !=null && !string.IsNullOrEmpty(graphSerializableNodeData.Guid))
            {
                m_Guid = graphSerializableNodeData.Guid;
            }
            else
            {
                m_Guid = System.Guid.NewGuid().ToString();
            }

            CreatePorts(inputContainer, outputContainer);

            titleContainer.Clear();
            StylizeTitleContainer(titleContainer);
            var container = CreateTitleContent(m_NodeProperty.NodeType);
            titleContainer.Add(container);

            SetPosition(new Rect(pos, DefaultNodeSize));

            DrawSpecialType();

            DrawProperties();
        }

        private void DrawSpecialType()
        {
            if (m_NodeProperty.NodeType == BTNodeType.Root)
            {
                //capabilities &= ~Capabilities.Movable;
                capabilities &= ~Capabilities.Deletable;
            }
        }

        #region Port
        private void CreatePorts(VisualElement inputContainer, VisualElement outputContainer)
        {
            if (m_NodeProperty.CapacityIn != BTPortCapacity.None)
            {
                CreateInputPort(inputContainer, string.Empty);
            }

            if (m_NodeProperty.CapacityOut != BTPortCapacity.None)
            {
                CreateOutputPort(outputContainer, string.Empty,
                     m_NodeProperty.CapacityOut == BTPortCapacity.Single ? Port.Capacity.Single : Port.Capacity.Multi);
            }
        }

        private Port CreateInputPort(VisualElement inputContainer, string name = "In")
        {
            var port = CreatePort(Direction.Input);
            port.portName = name;
            inputContainer.Add(port);

            RefreshExpandedState();
            RefreshPorts();

            return port;
        }

        private Port CreateOutputPort(VisualElement outputContainer, string name = "Out", Port.Capacity capacity = Port.Capacity.Single)
        {
            var port = CreatePort(Direction.Output, capacity);
            port.portName = name;
            outputContainer.Add(port);

            RefreshExpandedState();
            RefreshPorts();

            return port;
        }

        private Port CreatePort(Direction direction, Port.Capacity capacity = Port.Capacity.Single)
        {
            return InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(float));
        }
        #endregion

        #region Title
        private void StylizeTitleContainer(VisualElement container)
        {
            container.style.justifyContent = Justify.Center;
            container.style.paddingLeft = 5;
            container.style.paddingRight = 5;
        }

        private VisualElement CreateTitleContent(BTNodeType type)
        {
            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;

            var icon = new Image
            {
                image = GetIcon(type),
                scaleMode = ScaleMode.ScaleToFit
            };
            icon.style.marginRight = 5;
            container.Add(icon);

            var titleLabel = new Label(m_NodeProperty.Name);
            titleLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
            titleLabel.style.fontSize = 14;
            container.Add(titleLabel);

            return container;
        }

        protected virtual Texture2D GetIcon(BTNodeType type)
        {
            return Resources.Load<Texture2D>($"Icons/{GetIconFileName(type)}");
        }

        private string GetIconFileName(BTNodeType type)
        {
            switch (type)
            {
                case BTNodeType.Root:
                    return "root";
                case BTNodeType.Sequencer:
                    return "sequencer";
                case BTNodeType.Selector:
                    return "selector";
                default:
                    return string.Empty;
            }
        }

        #endregion

        #region Property
        private void DrawProperties()
        {

            if (m_NodeProperty.PropertyScript == null) return;

            m_PropertyContainer = new VisualElement();
            FieldInfo[] fieldInfoList = m_NodeProperty.PropertyScript.GetClass().GetFields();

            Action<object> bindDatAction = null;

            foreach (var fieldInfo in fieldInfoList)
            {
                VisualElement childContainer = new VisualElement();
                childContainer.style.height = 20;
                childContainer.style.flexDirection = FlexDirection.Row;
                childContainer.style.marginTop = 5;
                childContainer.style.marginBottom = 5;
                childContainer.style.marginRight = 5;
                childContainer.style.marginLeft = 5;

                var label = new Label(fieldInfo.Name);
                label.style.width = 70;
                label.style.unityTextAlign = TextAnchor.MiddleLeft;
                label.style.fontSize = 12;
                label.style.whiteSpace = WhiteSpace.Normal;
                childContainer.Add(label);

                VisualElement field = DrawPropField(fieldInfo, ref bindDatAction);

                StylizePropField(field);
                childContainer.Add(field);



                m_PropertyContainer.Add(childContainer);
            }

            ActionNodePropetyConstructFunc = () =>
            {
                var prop = Activator.CreateInstance(typeof(T));
                bindDatAction?.Invoke(prop);
                return prop;
            };

            mainContainer.Add(m_PropertyContainer);
        }

        private VisualElement DrawPropField(FieldInfo fieldInfo, ref Action<object> bindDatAction)
        {
            var type = fieldInfo.FieldType;

            if (type == typeof(string))
            {
                //if (fieldInfo.Name.Contains("Tag"))
                //{
                //    var tagField = new TagField(string.Empty, "Untagged");
                //    tagField.value = (string)fieldInfo.GetValue(propFieldData);
                //    return (StylizePropField(tagField), prop => fieldInfo.SetValue(prop, tagField.value));
                //}

                var field = new TextField(string.Empty, 200, true, false, '*')
                {
                    value = (string)fieldInfo.GetValue(m_PropertyData)
                };
                bindDatAction += prop => fieldInfo.SetValue(prop, field.value);
                return field;
            }

            if (type == typeof(int))
            {
                var field = new IntegerField
                {
                    value = (int)fieldInfo.GetValue(m_PropertyData)
                };
                bindDatAction += prop => fieldInfo.SetValue(prop, field.value);
                return field;
            }

            if (type == typeof(float))
            {
                var field = new FloatField { value = (float)fieldInfo.GetValue(m_PropertyData) };
                bindDatAction += prop => fieldInfo.SetValue(prop, field.value);
                return field;
            }

            if (type == typeof(bool))
            {
                var field = new Toggle
                {
                    value = (bool)fieldInfo.GetValue(m_PropertyData)
                };
                bindDatAction += prop => fieldInfo.SetValue(prop, field.value);
                return field;
            }

            if (type == typeof(Vector3))
            {
                var field = new Vector3Field
                {
                    value = (Vector3)fieldInfo.GetValue(m_PropertyData)
                };
                bindDatAction += prop => fieldInfo.SetValue(prop, field.value);
                return field;
            }

            if (typeof(ScriptableObject).IsAssignableFrom(type) || type.IsInterface)
            {
                var field = new ObjectField() { objectType = type };
                bindDatAction += prop => fieldInfo.SetValue(prop, field.value);
                return field;
            }

            return new Label($"Unsupported type {type}");
        }

        private void StylizePropField(VisualElement field)
        {
            field.style.width = 100;
        }

        #endregion

        #region Save
        public void SetDataManager(DataManager dataManager)
        {
            m_DataManger = dataManager;
        }

        public void Save(BehaviorTreeDesignContainer designContainer)
        {
            string json = "";
            if (ActionNodePropetyConstructFunc != null)
            {

                T serializableProperty = (T)ActionNodePropetyConstructFunc();
                json = PackUserData(serializableProperty);
            }

            var graphSerializableNodeData = new GraphSerializableNodeData(m_NodeProperty.Name, GetPosition().position, m_Guid, GetParentGuid(inputContainer), m_NodeProperty.NodeType, json);
            designContainer.AddNodeData(graphSerializableNodeData);
        }

      
        protected string GetParentGuid(VisualElement inputContainer)
        {
            if (inputContainer.childCount == 0)
            {
                return string.Empty;
            }

            Port inputPort = inputContainer[0] as Port;

            if (!inputPort.connected)
            {
                return string.Empty;
            }

            var edge = inputPort.connections.First(edge => edge.output != inputPort);
            var parentNode = edge.output.node;
            return (parentNode as ISavable).Guid;
        }

        public void UnpackPropertyJson()
        {
            m_PropertyData = JsonUtility.FromJson<T>(m_GraphSerializableNodeData.PropertyJson);
        }

        public string PackUserData(SerializableProperty nodeProperty)
        {
            String json = JsonUtility.ToJson(nodeProperty);
            return json;
        }
        #endregion
    }

}

