using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{
    /// <summary>
    /// »ù´¡½Úµã
    /// </summary>
    public class BTNull: BTBaseNode
    {
        private INode m_Child;

        public new BTNodeType NodeType => BTNodeType.Null;

        public override bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            Assert.IsTrue(children.Length == 1, "BaseNode can only have one child");
            if (children.Length != 1) return false;

            m_Child = children[0];

            return true;
        }

        public override void Execute()
        {
            m_Child.Enter();
        }
    }
}
