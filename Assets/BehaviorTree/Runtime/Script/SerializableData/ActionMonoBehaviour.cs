using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class ActionMonoBehaviour : MonoBehaviour
    {
        protected GameObject m_Actor; 

        public virtual void Init(GameObject actor)
        {
            m_Actor = actor;
        }

        public virtual BTNodeState Tick()
        {
            return BTNodeState.SUCCESS;
        }
    }
}
