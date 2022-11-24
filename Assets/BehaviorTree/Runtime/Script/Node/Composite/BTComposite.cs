using System;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public abstract class BTComposite : INode
    {
        protected INode[] m_Children;

        public virtual BTNodeType NodeType => throw new NotImplementedException();

        public virtual bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            if (children.Length == 0)
            {
                return false;
            }

            m_Children = children;

            return true;
        }

        public virtual BTNodeState Tick()
        {
            throw new NotImplementedException();
        }
    }
}