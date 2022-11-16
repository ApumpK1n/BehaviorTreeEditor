using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public static class BTGraphNodeFactory
    {
        public static Node CreateNodeGeneric<T>(Vector2 pos, string name) where T : IBTGraphNodeData, new()
        {
            return new BTGraphNode<T>(pos, name);
        }

        public static Node CreateActionNodeGeneric<T>(Vector2 pos, string name) where T : SerializableProperty, new()
        {
            return new BTGraphActionNode<T>(pos, name);
        }


        public static Node CreateNode(BTNodeType nodeType, Vector2 pos, string name, string guid)
        {
            switch (nodeType)
            {
                case BTNodeType.Root:
                    return new BTGraphNode<BTGraphRootData>(pos, name, guid);
                case BTNodeType.Sequencer:
                    return new BTGraphNode<BTGraphSequencerData>(pos, name, guid);
                case BTNodeType.Selector:
                    return new BTGraphNode<BTGraphSelectorData>(pos, name, guid);
                default:
                    return new BTGraphNode<BTGraphNullData>(pos, name, guid);
            }
        }

        public static Node CreateNode(Vector2 pos, string name, string guid, PropertySerializable property)
        {
            Type type = Type.GetType(property.PropertyType);

            dynamic propertyData = Convert.ChangeType(property, type);

            var leafType = typeof(BTGraphActionNode<>).MakeGenericType(type);
            return System.Activator.CreateInstance(leafType, new object[] { pos, name, guid, propertyData }) as Node;
        }
    }
}
