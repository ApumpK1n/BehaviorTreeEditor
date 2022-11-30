using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTSelector: BTComposite
    {
        public override BTNodeType NodeType => BTNodeType.Selector;

        private int m_CurrentIndex = -1;

        public override void Execute()
        {
            foreach (INode child in m_Children)
            {
                Assert.AreNotEqual(child.CurrentState, BTNodeState.ACTIVE);
            }

            m_CurrentIndex = -1;

            ProcessChildren();
        }

        private void ProcessChildren()
        {
            if (++m_CurrentIndex < m_Children.Length)
            {

                m_Children[m_CurrentIndex].Enter();
            }
            else
            {
                Exit(false);
            }
        }

        protected override void OnChildExited(INode child, bool result)
        {
            // find a success node, if not continue tick
            if (result)
            {
                Exit(true);
            }
            else
            {
                ProcessChildren();
            }
        }
    }


}
