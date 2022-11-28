using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Pumpkin.AI.BehaviorTree
{

    public class MoveToBehaviourScript : ActionScript
    {
        private Vector3 m_TestPos;
        private float speed = 5;
        private NavMeshAgent m_Agent;

        public override void Init(GameObject actor)
        {
            base.Init(actor);

            int area = 1 << NavMesh.GetAreaFromName("Walkable");
            NavMeshHit hit;
            NavMesh.SamplePosition(new Vector3(1, 0.5f, 0), out hit, 1.0f, area);
            m_TestPos = hit.position;
            m_Agent = actor.GetComponent<NavMeshAgent>();
        }

        public override bool Execute()
        {
            if (m_Actor.transform.position == m_TestPos)
            {
                return true;
            }

            m_Agent.SetDestination(m_TestPos);
            return false;
        }
    }

}
