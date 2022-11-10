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


        public static Node CreateNode(BTNodeType nodeType, Vector2 pos)
        {
            switch (nodeType)
            {
                case BTNodeType.Root:
                    return new BTGraphNode<BTGraphRootData>(pos);
                case BTNodeType.Sequencer:
                    return new BTGraphNode<BTGraphSequencerData>(pos);
                case BTNodeType.Selector:
                    return new BTGraphNode<BTGraphSelectorData>(pos);
                default:
                    return new BTGraphNode<BTGraphRootData>(pos);
            }
        }

        //public static Node CreateNode(BTBaseTask task, UnityEngine.Vector2 pos, string guid = "")
        //{
        //    var leafType = typeof(BTGraphNodeLeaf<>).MakeGenericType(task.GetType());
        //    return System.Activator.CreateInstance(leafType, new object[] { pos, guid }) as Node;
        //}
    }
}
