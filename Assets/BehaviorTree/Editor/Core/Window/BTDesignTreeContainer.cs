using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace Pumpkin.AI.BehaviorTree
{
    [CreateAssetMenu(fileName = "BehaviorTree_Design_Container", menuName = "Pumpkin/AI/BehaviorTree Design  Container")]
    public class BehaviorTreeDesignContainer : ScriptableObject
    {
        public List<GraphSerializableNodeData> nodeDataList = new List<GraphSerializableNodeData>();
        //public List<BTTaskData> taskDataList = new List<BTTaskData>();

        public void Save(UQueryState<Node> nodes)
        {
            nodeDataList.Clear();
            //taskDataList.Clear();
            nodes.ForEach(node => (node as ISavable).Save(this));
        }

    }
}

