using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public static class BTGraphNodeFactory
    {
        public static Node CreateNodeGeneric<T>(Vector2 pos) where T : IBTGraphNodeData, new()
        {
            return new BTGraphNode<T>(pos);
        }


        public static Node CreateNode(BTNodeType nodeType, Vector2 pos, string guid)
        {
            switch (nodeType)
            {
                case BTNodeType.Root:
                    return new BTGraphNode<BTGraphRootData>(pos, guid);
                case BTNodeType.Sequencer:
                    return new BTGraphNode<BTGraphSequencerData>(pos, guid);
                case BTNodeType.Selector:
                    return new BTGraphNode<BTGraphSelectorData>(pos, guid);
                default:
                    return new BTGraphNode<BTGraphRootData>(pos, guid);
            }
        }

        public static Node CreateActionNode<T>(Vector2 pos, string guid) where T : SerializableProperty
        {
            return new BTGraphNode<BTGraphActionData<T>>(pos, guid);
        }
    }
}
