using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTGraphDebugNode : Node
    {
        protected static Vector2 DefaultNodeSize = new Vector2(300f, 400f);
        protected NodeProperty m_NodeProperty;

        protected string m_Guid;

        private GraphSerializableNodeData m_GraphSerializableNodeData;

        private Image m_StatusIcon;
        private Color m_White = new Color(255, 255, 255);
        public string Guid => m_Guid;

        public BTGraphDebugNode(Vector2 pos, NodeProperty nodeProperty, GraphSerializableNodeData graphSerializableNodeData)
        {
            StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>(BTGraphDefaultConfig.GraphNodeStyleSheetPath);

            styleSheets.Add(style);
            AddToClassList("bold-text");

            m_NodeProperty = nodeProperty;
            m_GraphSerializableNodeData = graphSerializableNodeData;

            if (graphSerializableNodeData != null && !string.IsNullOrEmpty(graphSerializableNodeData.Guid))
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
            var container = CreateTitleContent();
            titleContainer.Add(container);

            SetPosition(new Rect(pos, DefaultNodeSize));

            capabilities &= ~Capabilities.Movable;
            capabilities &= ~Capabilities.Deletable;
            capabilities &= ~Capabilities.Copiable;
            capabilities &= ~Capabilities.Selectable;

            //SetEnabled(false);
        }

        #region Title
        private void StylizeTitleContainer(VisualElement container)
        {
            container.style.justifyContent = Justify.Center;
            container.style.paddingLeft = 5;
            container.style.paddingRight = 5;
        }

        private VisualElement CreateTitleContent()
        {
            var container = new VisualElement();
            container.style.flexDirection = FlexDirection.Row;

            var icon = new Image
            {
                image = GetIcon(),
                scaleMode = ScaleMode.ScaleToFit
            };
            icon.style.marginRight = 5;
            container.Add(icon);

            var titleLabel = new Label(m_NodeProperty.Name);
            titleLabel.style.unityTextAlign = TextAnchor.MiddleCenter;
            titleLabel.style.fontSize = 14;
            container.Add(titleLabel);

            m_StatusIcon = new Image()
            {
                style =
                {
                    width = 25,
                    height = 25,
                    marginRight = 5,
                }
            };

            m_StatusIcon.tintColor = m_White;
            container.Add(m_StatusIcon);
            return container;
        }

        protected virtual Texture2D GetIcon()
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>(m_NodeProperty.IconPath);
        }


        private void UpdateStatusIcon(Texture newImage)
        {
            if (newImage == null)
            {
                return;
            }

            m_StatusIcon.image = newImage;
        }
        #endregion

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
    }
}
