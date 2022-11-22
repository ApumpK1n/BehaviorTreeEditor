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
            ////if (data == null)
            ////{
            ////    DataFile = ScriptableObject.CreateInstance("SettingsData") as SettingsData;
            ////    CreateSettingsFile();
            ////}
            //else
            //{
            //    DataFile = data;
            //}
        }

        //private void CreateSettingsFile()
        //{
        //    if (!AssetDatabase.IsValidFolder(BehaviorTreeGraphWindow.c_RootPathData))
        //    {
        //        AssetDatabase.CreateFolder("Assets", "Behavior Tree Visualizer (Beta)");
        //        AssetDatabase.CreateFolder(BehaviorTreeGraphWindow.c_RootPathData, "Resources");
        //    }

        //    if (!AssetDatabase.Contains(DataFile))
        //    {
        //        AssetDatabase.CreateAsset(DataFile, $"{BehaviorTreeGraphWindow.c_DataPath}/settings.asset");
        //    }

        //    AssetDatabase.SaveAssets();
        //    AssetDatabase.Refresh();
        //}

       
    }
}
