using System;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Coroutine_Zenject {
    public class Coroutine_Controller
    {
        public static CancellationTokenSource GetCacellationToken()
        {
            return new CancellationTokenSource();
        }

        public static async void WaitToDo(Action action,float delay, CancellationToken token)
        {
            try {
                //Debug.Log($"協程開始：delay{delay}秒");
                await Task.Delay((int)(delay * 1000),token);
                action?.Invoke();
            }catch(TaskCanceledException){
                //Debug.Log("協程取消");
            }
        }

        public static async void WaitToDo(Action action, float delay)
        {
            try
            {
                //Debug.Log($"協程開始：delay{delay}秒");
                await Task.Delay((int)(delay * 1000));
                action?.Invoke();
            }
            catch (TaskCanceledException)
            {
                //Debug.Log("協程取消");
            }
        }

    }

}
