using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{
    public abstract class BTComposite : BTBaseNode
    {
        public override bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            Assert.IsTrue(children.Length > 0, "Composite nodes (Selector, Sequence, Parallel) need at least one child!");
            base.Init(children, actor, json, propertyType);

            return true;
        }
    }
}