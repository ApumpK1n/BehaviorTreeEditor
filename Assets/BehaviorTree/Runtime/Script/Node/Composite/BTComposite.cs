using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{
    public abstract class BTComposite : BTBaseNode
    {
        public override bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            if (children.Length == 0)
            {
                return false;
            }
            Assert.IsTrue(children.Length > 0, "Composite nodes (Selector, Sequence, Parallel) need at least one child!");
            m_Children = children;

            return true;
        }

        public virtual BTNodeState Tick()
        {
            throw new NotImplementedException();
        }
    }
}