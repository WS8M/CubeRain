using System;
using System.Collections;
using UnityEngine;

public abstract class TemporaryObject : MonoBehaviour, IPoolable
{
    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _minLifeTime;

    public event Action<float> TimeElapsed;
    public event Action<IPoolable> Removed;
    
    protected abstract void OnRemoved();
    
    protected void StartLifeTime()
    {
        float time = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1);
        StartCoroutine(DestroyInTime(time));
    }
    
    private IEnumerator DestroyInTime(float time)
    {
        var wait = new WaitForFixedUpdate();
        var maxTime = time;

        while (time > 0)
        {
            time -= Time.deltaTime;
            TimeElapsed?.Invoke(time / maxTime);
            
            yield return wait;
        }

        OnRemoved();
        Removed?.Invoke(this);
    }
}