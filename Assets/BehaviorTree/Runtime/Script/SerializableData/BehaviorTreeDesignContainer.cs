using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

        public int Length => nodeDataList.Count;

#if UNITY_EDITOR
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
#endif
    }
}

