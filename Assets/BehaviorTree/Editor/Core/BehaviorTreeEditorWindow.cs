using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;

namespace Pumpkin.AI.BehaviorTree
{
    public class BehaviorTreeEditorWindow : GraphViewEditorWindow
    {
        private BTGraphView m_BTGraphView; // 逻辑图界面

        private BTBlackboard m_BTBlackboard;

        private BTEditorCreateNodeWindow m_CreateNodeWindow; // 创建节点界面

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

            //m_CreateNodeWindow = CreateNodeSearchWindow();


            //m_BTGraphView.Add(m_BTBlackboard);

            rootVisualElement.Add(m_BTGraphView);
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

        //private BTEditorCreateNodeWindow CreateNodeSearchWindow()
        //{
        //    var window = UnityEngine.ScriptableObject.CreateInstance<BTEditorCreateNodeWindow>();
        //    //graphView.nodeCreationRequest += context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), window);
        //    System.Func<Vector2, Vector2> contextToLocalMousePos = contextMousePos =>
        //    {
        //        var worldPos = rootVisualElement.ChangeCoordinatesTo(rootVisualElement.parent, contextMousePos - position.position);
        //        return graphView.WorldToLocal(worldPos);
        //    };

        //    window.Init(node => graphView.AddElement(node), contextToLocalMousePos);
        //    return window;
        //}
    }
}
