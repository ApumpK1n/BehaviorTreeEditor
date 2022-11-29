using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class DataManager
    {
        public BTGraphNodeConfig NodeConfigFile { get; private set; }

        private SerializedObject m_ConfigRef;

        public DataManager()
        {
            LoadSettingsFile();
            m_ConfigRef = new SerializedObject(NodeConfigFile);
        }

        public void LoadSettingsFile()
        {
            BTGraphNodeConfig data = AssetDatabase.LoadAssetAtPath<BTGraphNodeConfig>(BTGraphDefaultConfig.DefaultNodeConfigPath);

            NodeConfigFile = data;
        }
       
    }
}
