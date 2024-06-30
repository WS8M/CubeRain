using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private SpawnPosition _spawnPosition;
    [Space]
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _repeatRate;
    
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCubeFromPool),0, _repeatRate);
    }
    
    private void GetCubeFromPool()
    {
        Cube cube = _pool.Get();
        cube.Removed += RemoveCube;
    }

    private void RemoveCube(Cube cube)
    {
        _pool.Release(cube);
        cube.Removed -= RemoveCube;
    }
    
    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = _spawnPosition.GetPosition();
        cube.transform.rotation = Quaternion.identity;
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        cube.gameObject.SetActive(true);
    }
    
    [Serializable]
    private class SpawnPosition
    {
        [SerializeField] private float _maxPositionX;
        [SerializeField] private float _minPositionX;
        [SerializeField] private float _maxPositionZ;
        [SerializeField] private float _minPositionZ;
        [SerializeField] private float _positionY;

        public Vector3 GetPosition()
        {
            var positionX = Random.Range(_minPositionX, _maxPositionX + 1);
            var positionZ = Random.Range(_minPositionZ, _maxPositionZ + 1);
            
            return new Vector3(positionX, _positionY, positionZ);
        }
    }
}