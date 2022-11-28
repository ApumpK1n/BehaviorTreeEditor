using System;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class SerializableProperty
    {

        protected GameObject m_Actor;
        protected INode m_Parent;

        public virtual void Init(GameObject actor, INode parent)
        {
            m_Actor = actor;
            m_Parent = parent;
        }

        public virtual void Execute()
        {
        }
    }
}
