using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTGraphActionNode<T> : BTGraphNode<BTGraphActionData<T>>, ISavable where T : SerializableProperty, new()
    {
        private VisualElement m_PropertyContainer;

        private Func<object> ActionNodePropetyConstructFunc;

        private T m_PropertyData;

        public BTGraphActionNode(Vector2 pos, string guid = "", T property = null) : base(pos, guid)
        {
            mainContainer.style.backgroundColor = new StyleColor(new Color(50f / 255f, 50f / 255f, 50f / 255f));

            if (property != null) m_PropertyData = property;
            else m_PropertyData = new T();

            DrawProperties();
        }

        private void DrawProperties()
        {

            m_PropertyContainer = new VisualElement();
            FieldInfo[] fieldInfoList = m_PropertyData.GetType().GetFields();

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

                VisualElement field = DrawPropField(fieldInfo, m_PropertyData, ref bindDatAction);

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

        private VisualElement DrawPropField(FieldInfo fieldInfo, object propFieldData, ref Action<object> bindDatAction)
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
                    value = (string)fieldInfo.GetValue(propFieldData)
                };
                bindDatAction += prop => fieldInfo.SetValue(prop, field.value);
                return field;
            }

            if (type == typeof(int))
            {
                var field = new IntegerField
                {
                    value = (int)fieldInfo.GetValue(propFieldData)
                };
                bindDatAction += prop => fieldInfo.SetValue(prop, field.value);
                return field;
            }

            if (type == typeof(float))
            {
                var field = new FloatField { value = (float)fieldInfo.GetValue(propFieldData) };
                bindDatAction += prop => fieldInfo.SetValue(prop, field.value);
                return field;
            }

            if (type == typeof(bool))
            {
                var field = new Toggle
                {
                    value = (bool)fieldInfo.GetValue(propFieldData)
                };
                bindDatAction += prop => fieldInfo.SetValue(prop, field.value);
                return field;
            }

            if (type == typeof(Vector3))
            {
                var field = new Vector3Field
                {
                    value = (Vector3)fieldInfo.GetValue(propFieldData)
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

        public override void Save(BehaviorTreeDesignContainer designContainer)
        {

            T serializableProperty = (T)ActionNodePropetyConstructFunc();

            designContainer.AddNodeData(
               new GraphSerializableNodeData(GetPosition().position, m_Guid, GetParentGuid(inputContainer), m_NodeData.NodeType, serializableProperty));
        }
    }
}
