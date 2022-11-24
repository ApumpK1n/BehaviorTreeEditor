using System;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    /// <summary>
    /// »ù´¡½Úµã
    /// </summary>
    public class BTNull: INode
    {
        private INode m_Child;

        public BTNodeType NodeType => BTNodeType.Null;

        public bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            if (children.Length != 1) return false;

            m_Child = children[0];

            return true;
        }

        public BTNodeState Tick()
        {
            return m_Child.Tick();
        }
    }
}
