using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BehaviorTree : MonoBehaviour
    {
        public bool Enabled { get; set; }

        [SerializeField]
        private GameObject m_Actor;

        [SerializeField]
        private BehaviorTreeDesignContainer m_DesignContainer;

        private INode m_Root;

        private BTNodeState m_CurrentState;

        private void Awake()
        {

            if (m_DesignContainer != null)
            {
                m_Root = ExtractTree();
            }
            Enabled = true;
        }

        private void Start()
        {
            if (m_Root != null && Enabled)
            {
                m_CurrentState = m_Root.Tick();
            }
        }

        private void Update()
        {
           
        }

        private INode ExtractTree()
        {
            INode root = null;
            var linkDict = new Dictionary<INode, List<INode>>(m_DesignContainer.Length);
            var linkDataList = new List<GraphLinkData>(m_DesignContainer.Length);
            var nodeDict = new Dictionary<string, INode>(m_DesignContainer.Length);

            var nodePropertyDict = new Dictionary<INode, GraphSerializableNodeData>(m_DesignContainer.Length);
            foreach (var nodeData in m_DesignContainer.NodeDataList)
            {
                INode node = BTNodeFactory.CreateNode(nodeData.NodeType);

                if (nodeData.NodeType == BTNodeType.Root)
                {
                    root = node;
                }
                linkDict.Add(node, new List<INode>());
                nodeDict.Add(nodeData.Guid, node);
                nodePropertyDict.Add(node, nodeData);
                if (!string.IsNullOrEmpty(nodeData.ParentGuid))
                {
                    linkDataList.Add(new GraphLinkData() { startGuid = nodeData.ParentGuid, endGuid = nodeData.Guid });
                }
            }

            foreach(var linkData in linkDataList)
            {
                var parent = nodeDict[linkData.startGuid];
                var child = nodeDict[linkData.endGuid];

                var children = linkDict[parent];
                children.Add(child);
                //children.Sort(nodePriorityComparer);
            }

            foreach (var keyValue in linkDict)
            {
                INode node = keyValue.Key;
                var nodeData = nodePropertyDict[node];
                Type propertyType = Type.GetType(nodeData.PropertyType);
                node.Init(keyValue.Value.ToArray(), m_Actor, nodeData.PropertyJson, propertyType);
            }

            if (root == null)
            {
                Debug.LogWarning($"BehaviorTree:{m_DesignContainer} not correct, dont have root node!");
            }

            return root;
        }
    }
}
