using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Linq;


namespace Pumpkin.AI.BehaviorTree
{
    public enum BTPortCapacity
    {
        None,
        Single,
        Multi
    }

    public class BTGraphNode<T> : Node where T : IBTGraphNodeData, new()
    {
        private static Vector2 DefaultNodeSize = new Vector2(300f, 400f);
        protected T m_NodeData;


        public BTGraphNode(Vector2 pos)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/BTGraphNode"));
            AddToClassList("bold-text");

            m_NodeData = new T();

            CreatePorts(inputContainer, outputContainer);

            titleContainer.Clear();
            StylizeTitleContainer(titleContainer);
            var container = CreateTitleContent(m_NodeData.NodeType);
            titleContainer.Add(container);

            SetPosition(new Rect(pos, DefaultNodeSize));

            if (m_NodeData.NodeType == BTNodeType.Root)
            {
                capabilities &= ~Capabilities.Movable;
                capabilities &= ~Capabilities.Deletable;
            }
        }

        #region Port
        private void CreatePorts(VisualElement inputContainer, VisualElement outputContainer)
        {
            if (m_NodeData.Capacity.In != BTPortCapacity.None)
            {
                CreateInputPort(inputContainer, string.Empty);
            }

            if (m_NodeData.Capacity.Out != BTPortCapacity.None)
            {
                CreateOutputPort(outputContainer, string.Empty,
                     m_NodeData.Capacity.Out == BTPortCapacity.Single ? Port.Capacity.Single : Port.Capacity.Multi);
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

            var icon = new Image();
            icon.image = GetIcon(type);
            icon.scaleMode = ScaleMode.ScaleToFit;
            icon.style.marginRight = 5;
            container.Add(icon);

            var titleLabel = new Label(m_NodeData.Name);
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
    }
}

