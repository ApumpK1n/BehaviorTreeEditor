using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Pumpkin.AI.BehaviorTree
{ 
    public class Clock
    {
        private Dictionary<Action, Timer> m_Timers = new Dictionary<Action, Timer>();
        private HashSet<Action> m_RemoveTimers = new HashSet<Action>();
        private Dictionary<Action, Timer> m_AddTimers = new Dictionary<Action, Timer>();
        private List<Timer> m_TimerPool = new List<Timer>();

        private bool m_IsInUpdate = false;
        private double m_ElapsedTime = 0f;
        private int m_CurrentTimerPoolIndex = 0;

        public int DebugPoolSize
        {
            get
            {
                return m_TimerPool.Count;
            }
        }

        class Timer
        {
            public double scheduledTime = 0f;
            public int repeat = 0;
            public bool used = false;
            public double delay = 0f;
            public float randomVariance = 0.0f;

            public void ScheduleAbsoluteTime(double m_ElapsedTime)
            {
                scheduledTime = m_ElapsedTime + delay - randomVariance * 0.5f + randomVariance * UnityEngine.Random.value;
            }
        }

        /// <summary>Register a timer function</summary>
        /// <param name="time">time in milliseconds</param>
        /// <param name="repeat">number of times to repeat, set to -1 to repeat until unregistered.</param>
        /// <param name="action">method to invoke</param>
        public void AddTimer(float time, int repeat, Action action)
        {
            AddTimer(time, 0f, repeat, action);
        }

        /// <summary>Register a timer function with random variance</summary>
        /// <param name="time">time in milliseconds</param>
        /// <param name="randomVariance">deviate from time on a random basis</param>
        /// <param name="repeat">number of times to repeat, set to -1 to repeat until unregistered.</param>
        /// <param name="action">method to invoke</param>
        public void AddTimer(float delay, float randomVariance, int repeat, Action action)
        {

            Timer timer = null;

            if (!m_IsInUpdate)
            {
                if (!m_Timers.ContainsKey(action))
                {
                    m_Timers[action] = getTimerFromPool();
                }
                timer = m_Timers[action];
            }
            else
            {
                if (!m_AddTimers.ContainsKey(action))
                {
                    m_AddTimers[action] = getTimerFromPool();
                }
                timer = m_AddTimers[action];

                if (m_RemoveTimers.Contains(action))
                {
                    m_RemoveTimers.Remove(action);
                }
            }

            Assert.IsTrue(timer.used);
            timer.delay = delay;
            timer.randomVariance = randomVariance;
            timer.repeat = repeat;
            timer.ScheduleAbsoluteTime(m_ElapsedTime);
        }

        public void Update(float deltaTime)
        {
            m_ElapsedTime += deltaTime;
            m_IsInUpdate = true;

            Dictionary<Action, Timer>.KeyCollection keys = m_Timers.Keys;
            foreach (Action callback in keys)
            {
                if (m_RemoveTimers.Contains(callback))
                {
                    continue;
                }

                Timer timer = m_Timers[callback];
                if (timer.scheduledTime <= m_ElapsedTime)
                {
                    if (timer.repeat == 0)
                    {
                        RemoveTimer(callback);
                    }
                    else if (timer.repeat >= 0)
                    {
                        timer.repeat--;
                    }
                    callback.Invoke();
                    timer.ScheduleAbsoluteTime(m_ElapsedTime);
                }
            }

            foreach (Action action in m_AddTimers.Keys)
            {
                if (m_Timers.ContainsKey(action))
                {
                    Assert.AreNotEqual(m_Timers[action], m_AddTimers[action]);
                    m_Timers[action].used = false;
                }
                Assert.IsTrue(m_AddTimers[action].used);
                m_Timers[action] = m_AddTimers[action];
            }
            foreach (Action action in m_RemoveTimers)
            {
                Assert.IsTrue(m_Timers[action].used);
                m_Timers[action].used = false;
                m_Timers.Remove(action);
            }


            m_AddTimers.Clear();
            m_RemoveTimers.Clear();


            m_IsInUpdate = false;
        }

        public void RemoveTimer(Action action)
        {
            if (!m_IsInUpdate)
            {
                if (m_Timers.ContainsKey(action))
                {
                    m_Timers[action].used = false;
                    m_Timers.Remove(action);
                }
            }
            else
            {
                if (m_Timers.ContainsKey(action))
                {
                    m_RemoveTimers.Add(action);
                }
                if (m_AddTimers.ContainsKey(action))
                {
                    Assert.IsTrue(m_AddTimers[action].used);
                    m_AddTimers[action].used = false;
                    m_AddTimers.Remove(action);
                }
            }
        }

        private Timer getTimerFromPool()
        {
            int i = 0;
            int l = m_TimerPool.Count;
            Timer timer = null;
            while (i < l)
            {
                int timerIndex = (i + m_CurrentTimerPoolIndex) % l;
                if (!m_TimerPool[timerIndex].used)
                {
                    m_CurrentTimerPoolIndex = timerIndex;
                    timer = m_TimerPool[timerIndex];
                    break;
                }
                i++;
            }

            if (timer == null)
            {
                timer = new Timer();
                m_CurrentTimerPoolIndex = 0;
                m_TimerPool.Add(timer);
            }

            timer.used = true;
            return timer;
        }
    }
}
