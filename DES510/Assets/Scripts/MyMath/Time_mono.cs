using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyMath {
    public class Time_mono : MonoBehaviour
    {
        private int initSize = 20;

        private Queue<timer> inactive_timers = new Queue<timer>();
        private List<timer> active_timers = new List<timer>();
        private List<timer> active_fixedtimers = new List<timer>();
        private List<timer> active_unscaledtimers = new List<timer>();

        private static int ids;

        private void Awake()
        {
            for (int i = 0; i < initSize; i++)
            {
                inactive_timers.Enqueue(new timer());
            }
        }

        void Update()
        {
            for (int i = active_timers.Count-1; i >= 0; i--)
            {
                timer timer = active_timers[i];
                if (timer.Tick(TimingType.Normal)) {
                    inactive_timers.Enqueue(timer);
                    active_timers.RemoveAt(i);
                }
            }

            for (int i = active_unscaledtimers.Count-1; i >= 0; i--)
            {
                timer timer = active_unscaledtimers[i];
                if (timer.Tick(TimingType.Unscaled)) {
                    inactive_timers.Enqueue(timer);
                    active_unscaledtimers.RemoveAt(i);
                }
            }
        }

        private void FixedUpdate()
        {
            for (int i = active_fixedtimers.Count-1; i >= 0; i--)
            {
                timer timer = active_fixedtimers[i];
                if (timer.Tick(TimingType.Fixed))
                {
                    inactive_timers.Enqueue(timer);
                    active_fixedtimers.RemoveAt(i);
                }
            }
        }

        public int Timing(float duration, Action onTime, Action<float> onTick,TimingType type)
        {
            ids++;
            timer timer;
            if (inactive_timers.Count > 0)
            {
                timer = inactive_timers.Dequeue();
            }
            else
            {
                timer = new timer();
            }

            timer.Reset(ids,duration, onTime, onTick);

            switch (type)
            {
                case TimingType.Normal:
                    active_timers.Add(timer);
                    break;
                case TimingType.Fixed:
                    active_fixedtimers.Add(timer);
                    break;
                case TimingType.Unscaled:
                    active_unscaledtimers.Add(timer);
                    break;
                default:
                    active_timers.Add(timer);
                    break;
            }
            return ids;
        }

        public void StopTiming(int id, TimingType type)
        {
            int index = -1;
            switch (type)
            {
                case TimingType.Normal:
                    index = active_timers.FindIndex(x=>x.id==id);
                    if (index!=-1)
                    {
                        active_timers.RemoveAt(index);
                    }
                    break;
                case TimingType.Fixed:
                    index = active_fixedtimers.FindIndex(x => x.id == id);
                    if (index != -1)
                    {
                        active_fixedtimers.RemoveAt(index);
                    }
                    break;
                case TimingType.Unscaled:
                    index = active_unscaledtimers.FindIndex(x => x.id == id);
                    if (index != -1)
                    {
                        active_unscaledtimers.RemoveAt(index);
                    }
                    break;
                default:
                    break;
            }
        }

        private class timer
        {
            public int id;
            public float t;
            public float duration;
            public Action onTime;
            public Action<float> onTick;

            public void Reset(int id,float duration, Action onTime, Action<float> onTick)
            {
                this.id = id;
                this.t = 0;
                this.duration = duration;
                this.onTime = onTime;
                this.onTick = onTick;
            }

            public bool Tick(TimingType type)
            {
                switch (type)
                {
                    case TimingType.Normal:
                        t += UnityEngine.Time.deltaTime;
                        break;
                    case TimingType.Fixed:
                        t += UnityEngine.Time.fixedDeltaTime;
                        break;
                    case TimingType.Unscaled:
                        t += UnityEngine.Time.unscaledDeltaTime;
                        break;
                    default:
                        t += UnityEngine.Time.deltaTime;
                        break;
                }

                
                if (t >= duration)
                {
                    onTime?.Invoke();
                    return true;
                }
                else
                {
                    onTick?.Invoke(t);
                    return false;
                }
            }

        }

        public enum TimingType {
            Normal,
            Fixed,
            Unscaled
        }
    }

}

