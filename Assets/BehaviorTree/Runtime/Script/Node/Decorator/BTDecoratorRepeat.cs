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
                m_Child.Enter();
            }
            else
            {
                Exit(true);
            }
            
        }

        protected override void OnChildExited(INode child, bool result)
        {
            if (result)
            {
                if (m_Property.RepeatNum > 0 && ++m_CurrentRepeatNum >= m_Property.RepeatNum)
                {
                    Exit(true);
                }
                else
                {
                    m_Clock.AddTimer(0, 0, TickChild);
                }
            }
            else
            {
                Exit(false);
            }
        }

        private void TickChild()
        {
            m_Child.Enter();
        }
    }
}
