using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{

    public class MoveToBehaviourScript : ActionScript
    {
        private Vector3 m_TestPos;
        private float speed = 5;

        public override void Init(GameObject actor)
        {
            base.Init(actor);

            m_TestPos = new Vector3(1, 0, 0);
        }

        public override void Execute()
        {
            //var step = speed * Time.deltaTime; // calculate distance to move
            //m_Actor.transform.position = Vector3.MoveTowards(m_Actor.transform.position, m_TestPos, step);
            Debug.Log("Repeat");
        }
    }

}
