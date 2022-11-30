using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTDebugGraphView : DebugGraphView
    {

        private DataManager m_DataManager;
        private List<GraphElement> m_NodeGraphElements = new List<GraphElement>();

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if (startPort.node != port.node && startPort != port)
                {
                    compatiblePorts.Add(port);
                }
            });

            return compatiblePorts;
        }

        public void SetDataManager(DataManager dataManager)
        {
            m_DataManager = dataManager;
        }

        private void ParseNode(BehaviorTreeDesignContainer designContainer, 
            Dictionary<string, Node> nodeDict, List<GraphLinkData> linkDataList, INode treeNode)
        {
            GraphSerializableNodeData nodeData = designContainer.GetNodeDataByGuid(treeNode.Guid);
            NodeProperty nodeProperty = m_DataManager.NodeConfigFile.GetNodePropertyWithName(nodeData.Name);
            Node node = BTGraphNodeFactory.CreateDebugNode(nodeData.Position, nodeProperty, nodeData, treeNode);


            nodeDict.Add(nodeData.Guid, node);

            if (!String.IsNullOrEmpty(nodeData.ParentGuid))
            {
                linkDataList.Add(new GraphLinkData() { startGuid = nodeData.ParentGuid, endGuid = nodeData.Guid });
            }
            AddNodeGraphElement(node);

            if (treeNode.Children == null) return;
            foreach(var child in treeNode.Children)
            {
                ParseNode(designContainer, nodeDict, linkDataList, child);
            }
        }

        public void UpdateView(BehaviorTree tree)
        {
            BehaviorTreeDesignContainer designContainer = tree.DesignContainer;


            var nodeDict = new Dictionary<string, Node>(designContainer.NodeDataList.Count);
            var linkDataList = new List<GraphLinkData>(designContainer.NodeDataList.Count);

            INode treeNode = tree.Root;

            ParseNode(designContainer, nodeDict, linkDataList, treeNode);

            foreach (var linkData in linkDataList)
            {
                var (parent, child) = (nodeDict[linkData.startGuid], nodeDict[linkData.endGuid]);
                var edge = (child.inputContainer[0] as Port).ConnectTo(parent.outputContainer[0] as Port);
                AddNodeGraphElement(edge);
            }
        }

        public void AddNodeGraphElement(GraphElement element)
        {
            AddElement(element);
            element.SetEnabled(false);
            m_NodeGraphElements.Add(element);
        }
    }
}
