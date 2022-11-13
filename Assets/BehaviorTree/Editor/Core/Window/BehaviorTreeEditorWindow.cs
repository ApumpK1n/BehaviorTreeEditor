using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using System.Linq;

namespace Pumpkin.AI.BehaviorTree
{
    public class BehaviorTreeEditorWindow : GraphViewEditorWindow
    {
        private BTGraphView m_BTGraphView; // 逻辑图界面

        private BTBlackboard m_BTBlackboard;

        private BTEditorCreateNodeWindow m_CreateNodeWindow; // 创建节点界面

        private BehaviorTreeDesignContainer m_DesignContainer; // 树存储的文件

        private Toolbar m_Toolbar; // 工具栏

        [MenuItem("Window/Pumpkin/Behavior Tree")]
        public static void Init()
        {
            GetWindow<BehaviorTreeEditorWindow>("Behavior Tree");
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(m_BTGraphView);
            rootVisualElement.Remove(m_Toolbar);
        }

        private void CreateGUI()
        {
            m_BTGraphView = CreateGraphView();
            m_BTGraphView.graphViewChanged += GraphViewChanged;

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
            var emptyGraphView = new BTGraphView();
            emptyGraphView.StretchToParentSize();

            return emptyGraphView;
        }

        private BTBlackboard CreateBlackboard(BTGraphView graphView, string title, Rect rect)
        {
            var blackboard = new BTBlackboard(graphView) { title = title, scrollable = true };
            blackboard.SetPosition(rect);
            //blackboard.addItemRequested = bb => AddBlackboardItem(bb);
            return blackboard;
        }

        /// <summary>
        /// 创建选择节点界面
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

            window.Init(OnCreateNode, contextToLocalMousePos);
            return window;
        }

        /// <summary>
        /// 工具栏
        /// </summary>
        /// <returns></returns>
        private Toolbar CreateToolbar()
        {
            var toolbar = new Toolbar();

            var saveBtn = new Button(() =>
            {
                SaveNodes2Container();
            })
            { text = "Save Assets" };

            var objFieldContainer = new ObjectField
            {
                objectType = typeof(BehaviorTreeDesignContainer),
                allowSceneObjects = false,
                value = m_DesignContainer,
            };

            objFieldContainer.RegisterValueChangedCallback(v =>
            {
                m_DesignContainer = objFieldContainer.value as BehaviorTreeDesignContainer;
                if (m_DesignContainer != null)
                {
                    m_BTGraphView.UpdateView(m_DesignContainer);
                }
                else
                {
                    m_BTGraphView.UpdateView(null);
                }

            });

            toolbar.Add(saveBtn);
            toolbar.Add(objFieldContainer);

            return toolbar;
        }

        private void SaveNodes2Container()
        {
            if (m_DesignContainer != null)
            {
                m_DesignContainer.Clear();
                m_BTGraphView.SaveNodes(m_DesignContainer);
            }
        }

        private void OnCreateNode(Node node)
        {
            m_BTGraphView.AddNodeGraphElement(node);
        }

        private GraphViewChange GraphViewChanged(GraphViewChange graphViewChange)
        {

            if (graphViewChange.edgesToCreate != null && graphViewChange.edgesToCreate.Count > 0)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    m_BTGraphView.AddNodeGraphElement(edge);
                }
            }
          
            return graphViewChange;
        }
    }
}
