using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;
using System.Linq;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTEditorCreateNodeWindow : ScriptableObject, ISearchWindowProvider
    {
        private List<Type> taskTypes = new List<Type>();
        private Action<Node> OnEntrySelected;
        private Func<Vector2, Vector2> ContextToLocalMousePos;
        //private BTTaskReferenceContainer _taskRefContainer;
        private Texture2D m_Indentation;

        public void Init(Action<Node> entrySelectCallback, Func<Vector2, Vector2> contextToLocalMousePos)
        {
            OnEntrySelected = entrySelectCallback;
            ContextToLocalMousePos = contextToLocalMousePos;

            m_Indentation = new Texture2D(1, 1);
            m_Indentation.SetPixel(0, 0, new Color(0, 0, 0, 0));
            m_Indentation.Apply();

            //_taskRefContainer = Resources.Load<BTTaskReferenceContainer>(BTTaskReferenceContainer.TASK_REF_CONTAINER_PATH);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //foreach (var assembly in assemblies)
            //{
            //    var types = assembly.GetTypes()
            //                        .Where(type => typeof(BTBaseTask).IsAssignableFrom(type)
            //                                        && type != typeof(BTBaseTask)
            //                                        && !type.IsGenericType
            //                                        && type != typeof(BTTaskNull));

            //    taskTypes.AddRange(types);
            //}
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Add Node")),
                new SearchTreeGroupEntry(new GUIContent("Composite"), 1),
                new SearchTreeEntry(new GUIContent(BTGraphDefaultConfig.GraphSequencerName, m_Indentation))
                {
                    userData = typeof(BTGraphSequencerData),
                    level = 2
                },
                new SearchTreeEntry(new GUIContent(BTGraphDefaultConfig.GraphSelectorName, m_Indentation))
                {
                    userData = typeof(BTGraphSelectorData),
                    level = 2
                },
                new SearchTreeGroupEntry(new GUIContent("Action"), 1),
                new SearchTreeEntry(new GUIContent("Wait", m_Indentation))
                {
                    userData = typeof(BTGraphActionData<ActionWaitProperty>),
                    level = 2
                },
            };

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var nodeSpawnPos = ContextToLocalMousePos(context.screenMousePosition);
            var userDataType = searchTreeEntry.userData as Type;
            var nodeCreationMethodName = nameof(BTGraphNodeFactory.CreateNodeGeneric);
            var methodInfo = typeof(BTGraphNodeFactory).GetMethod(nodeCreationMethodName);
            var genericMethodInfo = methodInfo.MakeGenericMethod(userDataType);
            var node = genericMethodInfo.Invoke(null, new object[] { nodeSpawnPos});
            OnEntrySelected?.Invoke(node as Node);
            return true;
        }
    }

}

