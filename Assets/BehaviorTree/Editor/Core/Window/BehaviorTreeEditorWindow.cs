using System;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;

namespace Pumpkin.AI.BehaviorTree
{
    public class BehaviorTreeEditorWindow : GraphViewEditorWindow
    {
        private BTGraphView m_BTGraphView; // �߼�ͼ����

        private BTBlackboard m_BTBlackboard;

        private BTEditorCreateNodeWindow m_CreateNodeWindow; // �����ڵ����

        private BehaviorTreeDesignContainer m_DesignContainer; // ���洢���ļ�

        private Toolbar m_Toolbar; // ������

        [MenuItem("Window/Pumpkin/Behavior Tree")]
        public static void Init()
        {
            GetWindow<BehaviorTreeEditorWindow>("Behavior Tree");
        }

        private void CreateGUI()
        {
            m_BTGraphView = CreateGraphView();

            m_BTBlackboard = CreateBlackboard(m_BTGraphView, "Shared Variables", new Rect(10, 30, 250, 250));
            m_BTBlackboard.visible =true;

            m_CreateNodeWindow = CreateNodeSearchWindow(m_BTGraphView);


            //m_BTGraphView.Add(m_BTBlackboard);

            m_Toolbar = CreateToolbar();
            m_Toolbar.visible = true;

            rootVisualElement.Add(m_BTGraphView);
            rootVisualElement.Add(m_Toolbar);
        }

        private BTGraphView CreateGraphView()
        {
            if (Selection.activeGameObject == null)
            {
                var emptyGraphView = new BTGraphView();
                emptyGraphView.StretchToParentSize();
                return emptyGraphView;
            }
            return null;
            //var res = TryGetBehaviorTree(Selection.activeGameObject, out var BT);
            //var graphView = res ? new BTGraphView(BT.DesignContainer) : new BTGraphView();
            //graphView.StretchToParentSize();
            //return (graphView, res ? BT : null);
        }

        private BTBlackboard CreateBlackboard(BTGraphView graphView, string title, Rect rect)
        {
            var blackboard = new BTBlackboard(graphView) { title = title, scrollable = true };
            blackboard.SetPosition(rect);
            //blackboard.addItemRequested = bb => AddBlackboardItem(bb);
            return blackboard;
        }

        /// <summary>
        /// ����ѡ��ڵ����
        /// </summary>
        /// <param name="graphView"></param>
        /// <returns></returns>
        private BTEditorCreateNodeWindow CreateNodeSearchWindow(BTGraphView graphView)
        {
            var window = CreateInstance<BTEditorCreateNodeWindow>();
            
            //Add create Node Content 
            graphView.nodeCreationRequest += context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), window);
            
            Func<Vector2, Vector2> contextToLocalMousePos = contextMousePos =>
            {
                var worldPos = rootVisualElement.ChangeCoordinatesTo(rootVisualElement.parent, contextMousePos - position.position);
                return graphView.WorldToLocal(worldPos);
            };

            window.Init(node => graphView.AddElement(node), contextToLocalMousePos);
            return window;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        private Toolbar CreateToolbar()
        {
            var toolbar = new Toolbar();

            var saveBtn = new Button(() =>
            {
                if (m_DesignContainer != null)
                {
                    m_DesignContainer.Save(m_BTGraphView.nodes);
                }
            })
            { text = "Save Assets" };

            toolbar.Add(saveBtn);

            return toolbar;
        }
    }
}
