using System;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour {
    private static readonly Queue<Action> _executionQueue = new Queue<Action>();

    public static void Enqueue(Action action) {
        lock (_executionQueue) {
            _executionQueue.Enqueue(action);
        }
    }

    private void Update() {
        while (true) {
            Action nextAction;

            lock (_executionQueue) {
                if (_executionQueue.Count == 0) {
                    break;
                }

                nextAction = _executionQueue.Dequeue();
            }

            nextAction();
        }
    }
}
