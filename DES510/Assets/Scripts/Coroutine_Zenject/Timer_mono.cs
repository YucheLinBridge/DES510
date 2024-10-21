using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Coroutine_Zenject {
    public class Timer_mono : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private bool timmmingOnStart;
        public UnityEvent OnTimming, OnTimesUp;

        private void Start()
        {
            if (timmmingOnStart)
            {
                StartTiming();
            }
        }

        public void StartTiming()
        {
            OnTimming?.Invoke();

            Coroutine_Controller.WaitToDo(() => {
                OnTimesUp?.Invoke();
            },time);
        }
    }
}

