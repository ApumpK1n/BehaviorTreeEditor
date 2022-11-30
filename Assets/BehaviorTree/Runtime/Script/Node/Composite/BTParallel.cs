using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTParallel: BTComposite
    {
        public override BTNodeType NodeType => BTNodeType.Parallel;

        private ParallelProperty m_Property;

        private int m_RunningCount;
        private int m_SucceededCount;
        private int m_FailedCount;

        public override bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            bool state = base.Init(children, actor, json, propertyType);

            m_Property = Util.UnpackPropertyJson<ParallelProperty>(json);

            return state;
        }


        public override void Execute()
        {
            foreach (INode child in m_Children)
            {
                Assert.AreNotEqual(child.CurrentState, BTNodeState.ACTIVE);
            }

            m_RunningCount = 0;
            m_SucceededCount = 0;
            m_FailedCount = 0;

            foreach (INode child in m_Children)
            {
                m_RunningCount++;
                child.Enter();
            }
        }

        protected override void OnChildExited(INode child, bool succeeded)
        {
            m_RunningCount--;
            if (succeeded)
            {
                m_SucceededCount++;
            }
            else
            {
                m_FailedCount++;
            }

            bool allChildrenEntered = m_RunningCount + m_SucceededCount + m_FailedCount == m_Children.Length;
            if (!allChildrenEntered) return;

            bool success = true;
            switch (m_Property.Strategy)
            {
                case Strategy.FAIL_ON_ONE:
                    success = !(m_FailedCount > 0);
                    break;
                case Strategy.FAIL_ON_ALL:
                    success = !(m_FailedCount >= m_Children.Length);
                    break;
            }
            Exit(success);
        }

    }

}
