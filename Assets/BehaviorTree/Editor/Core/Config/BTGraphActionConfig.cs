using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [CreateAssetMenu(menuName = "Pumpkin/AI/BehaviorTree Action Config")]
    public class BTGraphActionConfig : ScriptableObject
    {

        [SerializeField]
        private List<ActionInfo> m_ActionConfigs;

        public List<ActionInfo> ActionInfos => m_ActionConfigs;

    }


    [Serializable]
    public struct ActionInfo
    {
        public string Name;
        public string PropertyType;
        public string IconPath;
    }
}
