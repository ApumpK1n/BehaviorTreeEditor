using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pumpkin.AI.BehaviorTree
{
    public class BTDecoratorRepeat: BTDecorator
    {
        private DecoratorRepeat m_Property;

        private int m_CurrentRepeatNum;

        public override bool Init(INode[] children, GameObject actor, string json, Type propertyType)
        {
            bool state = base.Init(children, actor, json, propertyType);

            m_Property = Util.UnpackPropertyJson<DecoratorRepeat>(json);

            return state;
        }

        public override void Execute()
        {
            if (m_Property.RepeatNum > 0)
            {
                m_CurrentRepeatNum = 0;
                m_Child.Execute();
            }
            else
            {
                Exit(true);
            }
        
            //switch (status)
            //{
            //    case BTNodeState.SUCCESS:
            //        return HandleSuccess(m_Property.RepeatNum);
            //    case BTNodeState.FAILURE:
            //        Halt();
            //        return status;
            //    case BTNodeState.RUNNING:
            //    default:
            //        return status;
            //}
        }

        private BTNodeState HandleSuccess(int n)
        {
            if (m_SuccessNum == n)
            {
                Halt();
                return BTNodeState.SUCCESS;
            }
            else
            {
                m_SuccessNum++;
                return BTNodeState.RUNNING;
            }
        }

        public void Halt()
        {
            m_SuccessNum = 0;
        }
    }
}
