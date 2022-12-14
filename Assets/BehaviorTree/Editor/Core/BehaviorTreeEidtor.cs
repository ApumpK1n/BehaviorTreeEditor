using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    [CustomEditor(typeof(BehaviorTree))]
    public class BehaviorTreeEidtor: Editor
    {
        private BehaviorTree m_Tree;
        private void OnEnable()
        {
            m_Tree = target as BehaviorTree;

            EditorApplication.update += RedrawView;
        }


        void RedrawView()
        {
            Repaint();

        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Debug"))
            {
                if (BehaviorTreeDebugWindow.Instance == null)
                {
                    BehaviorTreeDebugWindow.Init();
                }
                if (m_Tree != null && m_Tree.Root != null)
                {
                    BehaviorTreeDebugWindow.Instance.BuildBehaviorTree(m_Tree);
                }
            }

            if (GUILayout.Button("Tick"))
            {
                if (m_Tree != null)
                {
                    m_Tree.EnterBehaviorTree();
                }
            }
        }
    }
}
