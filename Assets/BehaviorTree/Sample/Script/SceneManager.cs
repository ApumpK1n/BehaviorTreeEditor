using System;
using System.Collections.Generic;
using Pumpkin.Utility;
using UnityEngine.AI;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class SceneManager : SingletonBehaviour<SceneManager>
    {

        public NavMeshSurface NavMeshSurface;
        public BehaviorTree m_BehaviorTree;

        public void Start()
        {
            m_BehaviorTree.EnterBehaviorTree();
        }
    }
}
