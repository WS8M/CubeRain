using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private ColorRandomizer _colorRandomizer;
    [SerializeField] private float _maxLifeTime;
    [SerializeField] private float _minLifeTime;

    private bool _isColorChanged;

    public Action<Cube> Removed;
    
    private void StartLifeTime()
    {
        float time = Random.Range(_minLifeTime, _maxLifeTime + 1);
        StartCoroutine(DestroyInTime(time));
    }

    private IEnumerator DestroyInTime(float time)
    {
        yield return new WaitForSeconds(time);
        ResetParameters();
        Removed?.Invoke(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Platform platform) && _isColorChanged == false)
        {
            _isColorChanged = true;
            _colorRandomizer.Set();
            StartLifeTime();
        }
    }
    
    private void ResetParameters()
    {
        _colorRandomizer.Restore();
        _isColorChanged = false;
    }
}