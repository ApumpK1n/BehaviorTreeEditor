using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public abstract class BTComposite<T> : BTBaseNode<T> where T : SerializableProperty
    {
        protected INode[] m_Children;
        protected string m_Json;

        public override bool Init(INode[] children, GameObject actor, string json)
        {
            if (children.Length == 0)
            {
                return false;
            }

            m_Children = children;
            m_Json = json;

            return true;
        }
    }
}