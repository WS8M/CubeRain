using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class SpawnZone
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