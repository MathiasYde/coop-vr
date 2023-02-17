using System;
using UnityEngine;

[Serializable]
public class Timer {
    private float timer;
    [SerializeField] private float timeout;

    public event Action onTimerEnd;

    public bool isFinished => timer >= timeout;
    public float remainingTime => timeout - timer;

    public Timer() { }
    public Timer(float time) {
        this.timeout = time;
    }

    public float GetTimeout() {
        return timeout;
    }

    public void SetTimeout(float timeout) {
        this.timeout = timeout;
    }

    public void Update(float deltaTime) {
        timer += deltaTime;
        if (isFinished) {
            onTimerEnd?.Invoke();
        }
    }

    public void Reset() {
        timer = 0.0f;
    }
}