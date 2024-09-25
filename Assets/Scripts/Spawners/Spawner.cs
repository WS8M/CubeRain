using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour, ISpawner where T : MonoBehaviour, IPoolable
{
    [SerializeField] protected T Prefab;
    
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<IPoolable> _pool;
    
    public event Action Created;
    public event Action Spawned;
    
    private void Awake()
    {
        _pool = new ObjectPool<IPoolable>(
            createFunc: CreateFunction,
            actionOnGet: ActionOnGet,
            actionOnRelease: ActionOnRelease,
            actionOnDestroy: obj => Destroy((obj as T).gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }
    
    public int GetCountActive()
    {
        return _pool.CountActive;
    }

    protected abstract T OnCreate();
    
    protected abstract void OnGet(T obj);
    
    protected abstract void OnRelease(T obj);
    
    protected void GetObjectFromPool()
    {
        var obj = _pool.Get() as T;
        obj.Removed += RemoveObject;
    }

    private void RemoveObject(IPoolable obj)
    {
        _pool.Release(obj as T);
        obj.Removed -= RemoveObject;
    }
    
    private T CreateFunction()
    {
        Created?.Invoke();
        return OnCreate();
    }

    private void ActionOnGet(IPoolable obj)
    {
        Spawned?.Invoke();
        OnGet(obj as T);
    }
    
    private void ActionOnRelease(IPoolable obj)
    {
        OnRelease(obj as T);
    }
}