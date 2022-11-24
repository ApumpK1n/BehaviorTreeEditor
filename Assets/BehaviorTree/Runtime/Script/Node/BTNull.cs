using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    /// <summary>
    /// »ù´¡½Úµã
    /// </summary>
    public class BTNull<T>: BTBaseNode<T> where T : SerializableProperty
    {
        private INode[] m_Children;

        public override BTNodeType NodeType => BTNodeType.Null;

        public override bool Init(INode[] children, GameObject actor, string json)
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
