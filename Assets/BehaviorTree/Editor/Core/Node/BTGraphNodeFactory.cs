using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public static class BTGraphNodeFactory
    {
        public static Node CreateNodeGeneric<T>(Vector2 pos, NodeProperty nodeProperty, GraphSerializableNodeData graphSerializableNodeData) where T : SerializableProperty, new()
        {
            return new BTGraphNode<T>(pos, nodeProperty, graphSerializableNodeData);
        }


        public static Node CreateNode(Vector2 pos, NodeProperty nodeProperty, GraphSerializableNodeData graphSerializableNodeData)
        {

            Type propertyType = typeof(SerializableProperty);
            if (nodeProperty.PropertyScript != null)
            {
                propertyType = nodeProperty.PropertyScript.GetClass();
            }
            var nodeCreationMethodName = nameof(BTGraphNodeFactory.CreateNodeGeneric);
            var methodInfo = typeof(BTGraphNodeFactory).GetMethod(nodeCreationMethodName);
            var genericMethodInfo = methodInfo.MakeGenericMethod(propertyType);

            object node = genericMethodInfo.Invoke(null, new object[] { pos, nodeProperty, graphSerializableNodeData }); ;
            return node as Node;
        }

        public static Node CreateDebugNode(Vector2 pos, NodeProperty nodeProperty, GraphSerializableNodeData graphSerializableNodeData, INode treeNode)
        {
            return new BTGraphDebugNode(pos, nodeProperty, graphSerializableNodeData, treeNode);
        }
    }
}
