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

        public static Node CreateActionNodeGeneric<T>(Vector2 pos, string name) where T : SerializableProperty, new()
        {
            return new BTGraphActionNode<T>(pos);
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

        public static Node CreateNode(Vector2 pos, string guid, SerializableProperty property)
        {
            var leafType = typeof(BTGraphActionNode<>).MakeGenericType(property.GetType());
            return System.Activator.CreateInstance(leafType, new object[] { pos, guid, property }) as Node;
        }
    }
}
