using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [FormerlySerializedAs("_spawnPosition")] [SerializeField] private SpawnZone spawnZone;
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
        cube.transform.position = spawnZone.GetPosition();
        cube.transform.rotation = Quaternion.identity;
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        cube.gameObject.SetActive(true);
    }
}