using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTSequencer : BTComposite
    {
        public override BTNodeType NodeType => BTNodeType.Sequencer;

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
                Exit(true);
            }
        }

        protected override void OnChildExited(INode child, bool result)
        {
            Debug.Log(" BTSequencer OnChildExited:" + result);
            // if node return fail, exit sequence
            if (result)
            {
                ProcessChildren();
            }
            else
            {
                Exit(false);
            }
        }
    }

}
