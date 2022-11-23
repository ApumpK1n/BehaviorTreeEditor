using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{

    public class MoveToBehaviourScript : ActionMonoBehaviour
    {
        private Vector3 m_TestPos;
        private float speed = 5;

        public override void Init(GameObject actor)
        {
            base.Init(actor);

            m_TestPos = new Vector3(1, 0, 0);
        }

        public override BTNodeState Tick()
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, m_TestPos, step);
            return BTNodeState.SUCCESS;
        }
    }

}
