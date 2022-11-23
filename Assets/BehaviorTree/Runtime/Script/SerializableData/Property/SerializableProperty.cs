using System;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class SerializableProperty
    {

        private GameObject m_Actor;


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
