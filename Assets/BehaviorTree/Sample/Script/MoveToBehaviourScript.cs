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

        //private Vector3 point1 = new Vector3(1, 0.5f, -11);

        //private Vector3 point2 = new Vector3(-10, 0.5f, -10);


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
            if (Vector3.Distance(m_Actor.transform.position, m_TestPos) < 0.5f)
            {
                return true;
            }

            m_Agent.SetDestination(m_TestPos);
            return false;
        }
    }

}
