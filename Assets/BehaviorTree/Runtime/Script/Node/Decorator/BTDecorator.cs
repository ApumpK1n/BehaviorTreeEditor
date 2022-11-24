using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTDecorator : INode
    {
        protected INode m_Child;

        public BTNodeType NodeType => throw new NotImplementedException();

        public virtual bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            if (children.Length != 1)
            {
                Debug.LogWarning($"Decorator:{this} child count != 1");
                return false;
            }
            m_Child = children[0];

            return true;
        }

        public virtual BTNodeState Tick()
        {
            throw new NotImplementedException();
        }
    }
}
