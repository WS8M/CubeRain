using System;
using UnityEngine;

[RequireComponent(typeof(Spawner<TemporaryObject>))]
public class SpawnerStats: MonoBehaviour
{
    private ISpawner _spawner;

    public Action Update;
    
    public int NumberOfCreated { get; private set; }
    public int NumberOfSpawned { get; private set; }
    public int NumberOfActive => _spawner.GetCountActive(); 
    
    private void Awake()
    {
        _spawner = GetComponent<ISpawner>();
    }

    private void OnEnable()
    {
        _spawner.Created += OnCreated;
        _spawner.Spawned += OnSpawned;
    }
    
    private void OnDisable()
    {
        _spawner.Created -= OnCreated;
        _spawner.Spawned -= OnSpawned;
    }

    private void OnSpawned()
    {
        Debug.Log(nameof(OnSpawned));
        NumberOfSpawned++;
        Update?.Invoke();
    }

    private void OnCreated()
    {
        Debug.Log(nameof(OnCreated));

        NumberOfCreated++;
        Update?.Invoke();
    }
}