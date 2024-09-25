using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Vector3 _spawnPosition;
    
    public void Spawn(Vector3 position)
    {
        _spawnPosition = position;
        GetObjectFromPool();
    }

    protected override Bomb OnCreate()
    {
        return Instantiate(Prefab);
    }

    protected override void OnGet(Bomb obj)
    {
        obj.transform.position = _spawnPosition;
        obj.transform.rotation = Quaternion.identity;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        obj.gameObject.SetActive(true);
        obj.Get();
    }

    protected override void OnRelease(Bomb obj)
    {
        obj.gameObject.SetActive(false);
    }
}