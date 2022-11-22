using System;
using System.Collections.Generic;
using UnityEditor;

namespace Pumpkin.AI.BehaviorTree
{
    [Serializable]
    public class NodeProperty
    {
        public string Name;
        public BTNodeType NodeType;
        public NodeBelongTo NodeBelongTo;
        public BTPortCapacity CapacityIn;
        public BTPortCapacity CapacityOut;
        public string IconPath;
        public MonoScript PropertyScript = null;


    //public Color TitleBarColor = SettingsData.DefaultColor;
    //public Sprite Icon = null;
    //public bool IsDecorator = false;
    //public bool InvertResult = false;
}
}
