using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public abstract class BTComposite : BTBaseNode
    {
        protected BTBaseNode[] m_Children;

        public override bool Init(BTBaseNode[] children, GameObject actor)
        {
            if (children.Length == 0)
            {
                return false;
            }

            m_Children = children;

            return true;
        }
    }
}