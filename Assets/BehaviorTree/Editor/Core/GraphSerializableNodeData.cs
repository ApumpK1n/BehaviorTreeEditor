using System;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class GraphSerializableNodeData
    {
        [SerializeField]
        private BTNodeType m_NodeType = BTNodeType.Null;

        private Vector2 m_Position = Vector2.zero;

        private int m_NodeIndex = -1;

        private int m_NodeParentIndex = -1;

        public Vector2 Position => m_Position;
        public BTNodeType NodeType => m_NodeType;
        public int NodeIndex => m_NodeIndex;
        public int NodeParentIndex => m_NodeParentIndex;

        public GraphSerializableNodeData(Vector2 position, int nodeIndex, int nodeParentIndex, BTNodeType nodeType)
        {
            m_Position = position;
            m_NodeType = nodeType;

            m_NodeIndex = nodeIndex;
            m_NodeParentIndex = nodeParentIndex;
        }
    }
}
