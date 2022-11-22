using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BehaviorTree : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Actor;

        [SerializeField]
        private BehaviorTreeDesignContainer m_DesignContainer;

        private BTBaseNode m_Root;

        private void Awake()
        {
            if (m_DesignContainer != null)
            {
                m_Root = ExtractTree();
            }

        }


        private void Update()
        {
            if (m_Root != null)
            {
                m_Root.Tick();
            }
        }

        private BTBaseNode ExtractTree()
        {
            BTBaseNode root = null;
            var linkDict = new Dictionary<BTBaseNode, List<BTBaseNode>>(m_DesignContainer.Length);
            var linkDataList = new List<GraphLinkData>(m_DesignContainer.Length);
            var nodeDict = new Dictionary<string, BTBaseNode>(m_DesignContainer.Length);
            foreach (var nodeData in m_DesignContainer.NodeDataList)
            {
                var node = BTNodeFactory.Create(nodeData.NodeType);

                if (nodeData.NodeType == BTNodeType.Root)
                {
                    root = node;
                }
                linkDict.Add(node, new List<BTBaseNode>());
                nodeDict.Add(nodeData.Guid, node);
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
                keyValue.Key.Init(keyValue.Value.ToArray(), m_Actor);
            }

            if (root == null)
            {
                Debug.LogWarning($"BehaviorTree:{m_DesignContainer} not correct, dont have root node!");
            }

            return root;
        }
    }
}
