using System;
using UnityEngine;
using System.Collections;

namespace Helpers
{
    public class Timer
    {
        private readonly WaitForSeconds Second = new WaitForSeconds(1);

        private readonly bool Backward;

        private readonly float StopAt;

        private MonoBehaviour mono;

        private Coroutine routine;

        private float count;

        public Timer(float stop)
        {
            StopAt = stop;
        }

        public Timer(float stop, bool backward)
        {
            count = backward ? stop : 0;

            StopAt = backward ? 0 : stop;

            Backward = backward;
        }

        public Coroutine Start(Func<bool> stopWhen, Action<float> time, Action completed, MonoBehaviour mono)
        {
            this.mono = mono;

            return routine = mono.StartCoroutine(Routine(null, stopWhen, f => time?.Invoke(f), completed));
        }

        public void Stop()
        {
            if (mono)
            {
                mono.StopCoroutine(routine);
            }
        }

        private IEnumerator Routine(Action awake, Func<bool> stopWhen, Action<float> time, Action completed)
        {
            awake?.Invoke();

            while ((Backward ? (count >= StopAt) : (count <= StopAt)) && !stopWhen())
            {
                yield return Second;

                time?.Invoke(Backward ? count-- : count++);
            }

            if (!stopWhen())
            {
                completed?.Invoke();
            }

            routine = null;
        }
    }
}