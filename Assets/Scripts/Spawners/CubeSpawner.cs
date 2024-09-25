using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private SpawnZone _spawnZone;
    [SerializeField] private float _repeatRate;

    private void Start()
    {
        InvokeRepeating(nameof(GetObjectFromPool),0, _repeatRate);
    }

    protected override Cube OnCreate()
    {
        var cube = Instantiate(Prefab);
        cube.Init();
        return cube;
    }

    protected override void OnGet(Cube cube)
    {
        cube.transform.position = _spawnZone.GetPosition();
        cube.transform.rotation = Quaternion.identity;
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        cube.gameObject.SetActive(true);
    }

    protected override void OnRelease(Cube obj)
    {
        obj.gameObject.SetActive(false);
        _bombSpawner?.Spawn(obj.transform.position);
    }
}