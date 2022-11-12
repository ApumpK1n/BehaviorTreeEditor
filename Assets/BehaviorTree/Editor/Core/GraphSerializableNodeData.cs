﻿using System;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class GraphSerializableNodeData
    {
        [SerializeField]
        private BTNodeType m_NodeType;

        private Vector2 m_Position;

        private string m_NodeGuid;

        private string m_NodeParentGuid;

        public Vector2 Position => m_Position;
        public BTNodeType NodeType => m_NodeType;
        public string Guid => m_NodeGuid;
        public string ParentGuid => m_NodeParentGuid;

        public GraphSerializableNodeData(Vector2 position, string nodeGuid, string nodeParentguid, BTNodeType nodeType)
        {
            m_Position = position;
            m_NodeType = nodeType;

            m_NodeGuid = nodeGuid;
            m_NodeParentGuid = nodeParentguid;
        }
    }


    /// <summary>
    /// 节点连接数据
    /// </summary>
    public struct GraphLinkData
    {
        public string startGuid, endGuid;
    }
}