using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace Pumpkin.AI.BehaviorTree
{
    [CreateAssetMenu(menuName = "Pumpkin/AI/BehaviorTree Design Container")]
    public class BehaviorTreeDesignContainer : ScriptableObject
    {
        [SerializeField]
        private List<GraphSerializableNodeData> nodeDataList = new List<GraphSerializableNodeData>();

        public List<GraphSerializableNodeData> NodeDataList 
        { 
            get
            {
                return nodeDataList;
            }
        }
        //public List<BTTaskData> taskDataList = new List<BTTaskData>();

        public void Clear()
        {
            nodeDataList.Clear();
            //taskDataList.Clear();
        }


        public void AddNodeData(GraphSerializableNodeData graphSerializableNodeData)
        {
            nodeDataList.Add(graphSerializableNodeData);
        }
    }
}

