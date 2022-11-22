using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTGraphView : AbstractGraphView
    {
        private Vector2 DefaultRootSpawnPos = new Vector2(100f, 300f);

        private List<GraphElement> m_NodeGraphElements = new List<GraphElement>();
        private DataManager m_DataManager;

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

        public void SaveNodes(BehaviorTreeDesignContainer designContainer)
        {
            nodes.ForEach(node => (node as ISavable).Save(designContainer));
        }

        public void UpdateView(BehaviorTreeDesignContainer designContainer)
        {
            if (designContainer == null)
            {
                foreach(var element in m_NodeGraphElements)
                {
                    element.Clear();
                    RemoveElement(element);
                }
                m_NodeGraphElements.Clear();
                return;
            }
            m_NodeGraphElements.Clear();

            if (designContainer.NodeDataList == null || designContainer.NodeDataList.Count == 0)
            {
                NodeProperty nodeProperty = m_DataManager.NodeConfigFile.GetNodePropertyWithName(BTGraphDefaultConfig.GraphRootName);
                var root = BTGraphNodeFactory.CreateNode(DefaultRootSpawnPos, nodeProperty, null);
                AddNodeGraphElement(root);
                return;
            }

            var nodeDict = new Dictionary<string, Node>(designContainer.NodeDataList.Count);
            var linkDataList = new List<GraphLinkData>(designContainer.NodeDataList.Count);

            foreach (var nodeData in designContainer.NodeDataList)
            {
                NodeProperty nodeProperty = m_DataManager.NodeConfigFile.GetNodePropertyWithName(nodeData.Name);
                Node node = BTGraphNodeFactory.CreateNode(nodeData.Position, nodeProperty, nodeData);


                nodeDict.Add(nodeData.Guid, node);

                if (!String.IsNullOrEmpty(nodeData.ParentGuid))
                {
                    linkDataList.Add(new GraphLinkData() { startGuid = nodeData.ParentGuid, endGuid = nodeData.Guid });
                }

                AddNodeGraphElement(node);
            }
            foreach(var linkData in linkDataList)
            {
                var (parent, child) = (nodeDict[linkData.startGuid], nodeDict[linkData.endGuid]);
                var edge = (child.inputContainer[0] as Port).ConnectTo(parent.outputContainer[0] as Port);
                AddNodeGraphElement(edge);
            }

        }

        public void AddNodeGraphElement(GraphElement element)
        {
            AddElement(element);
            m_NodeGraphElements.Add(element);
        }
    }
}
