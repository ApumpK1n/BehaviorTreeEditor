using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

        public void Clear()
        {
            nodeDataList.Clear();
        }


        public void AddNodeData(GraphSerializableNodeData graphSerializableNodeData)
        {
            nodeDataList.Add(graphSerializableNodeData);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

