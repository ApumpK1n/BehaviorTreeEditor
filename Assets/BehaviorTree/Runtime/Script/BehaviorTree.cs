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

        private void Awake()
        {

            if (m_DesignContainer != null)
            {
                m_Root = ExtractTree();
            }
            Enabled = true;
        }


        private void Update()
        {
            if (m_Root != null && Enabled)
            {
                m_Root.Tick();
            }
        }

        private INode ExtractTree()
        {
            INode root = null;
            var linkDict = new Dictionary<INode, List<INode>>(m_DesignContainer.Length);
            var linkDataList = new List<GraphLinkData>(m_DesignContainer.Length);
            var nodeDict = new Dictionary<string, INode>(m_DesignContainer.Length);

            var nodePropertyDict = new Dictionary<INode, string>(m_DesignContainer.Length);
            foreach (var nodeData in m_DesignContainer.NodeDataList)
            {
                INode node = BTNodeFactory.CreateGeneric(nodeData.NodeType, nodeData.PropertyType);

                if (nodeData.NodeType == BTNodeType.Root)
                {
                    root = node;
                }
                linkDict.Add(node, new List<INode>());
                nodeDict.Add(nodeData.Guid, node);
                nodePropertyDict.Add(node, nodeData.PropertyJson);
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
                node.Init(keyValue.Value.ToArray(), m_Actor, nodePropertyDict[node]);
            }

            if (root == null)
            {
                Debug.LogWarning($"BehaviorTree:{m_DesignContainer} not correct, dont have root node!");
            }

            return root;
        }
    }
}
