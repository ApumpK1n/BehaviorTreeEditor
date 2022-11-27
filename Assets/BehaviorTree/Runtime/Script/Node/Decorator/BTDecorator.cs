using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTDecorator : BTBaseNode
    {
        protected INode m_Child;

        public override bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            Assert.IsTrue(children.Length == 1, "Decorator can only have one child");
            if (children.Length != 1)
            {
                Debug.LogWarning($"Decorator:{this} child count != 1");
                return false;
            }
            base.Init(children, actor, json, propertyType);
            m_Child = children[0];

            return true;
        }
    }
}
