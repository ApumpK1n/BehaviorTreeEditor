using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [CreateAssetMenu(menuName = "Pumpkin/AI/BehaviorTree Node Config")]
    public class BTGraphNodeConfig : ScriptableObject
    {
        public List<NodeProperty> MainStyleProperties = new List<NodeProperty>();


        public NodeProperty GetNodePropertyWithName(string name)
        {
            foreach(NodeProperty nodeProperty in MainStyleProperties)
            {
                if (nodeProperty.Name == name)
                {
                    return nodeProperty;
                }
            }
            Debug.LogWarning($"BTGraphNodeConfig GetNodePropertyWithName: {name} is not exist!");
            return null;
        }
    }
}
