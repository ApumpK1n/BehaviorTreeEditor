using System;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class GraphSerializableNodeData
    {
        public BTNodeType NodeType;
        public Vector2 Position;
        public string Name;
        public string Guid;
        public string ParentGuid;
        public string PropertyJson;

        public GraphSerializableNodeData(string name, Vector2 position, string nodeGuid, string nodeParentguid, BTNodeType nodeType, string propertyJson)
        {
            Name = name;
            Position = position;
            NodeType = nodeType;
            Guid = nodeGuid;
            ParentGuid = nodeParentguid;
            PropertyJson = propertyJson;
        }
    }

    /// <summary>
    /// 节点连接数据
    /// </summary>
    public struct GraphLinkData
    {
        public string startGuid, endGuid;
    }
}
