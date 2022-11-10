using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    /// <summary>
    /// »ù´¡½Úµã
    /// </summary>
    public class BTNull: BTBaseNode
    {
        private BTBaseNode[] m_Children;

        public override BTNodeType NodeType => BTNodeType.Null;

        public override bool Init(BTBaseNode[] children, GameObject actor)
        {
            if (children.Length < 1) return false;

            m_Children = children;

            return true;
        }

        public override BTNodeState Tick()
        {
            foreach(var child in m_Children)
            {
                child.Tick();
            }
            return BTNodeState.SUCCESS;
        }
    }
}
