using UnityEngine;

public class Cube : TemporaryObject
{
    [SerializeField] private MeshRenderer _meshRenderer;
    
    private ColorRandomizer _colorRandomizer;
    private bool _isColorChanged;

    public void Init() => 
        _colorRandomizer = new ColorRandomizer(_meshRenderer);

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Platform platform) && _isColorChanged == false)
        {
            _isColorChanged = true;
            _colorRandomizer.Set();
            StartLifeTime();
        }
    }

    protected override void OnRemoved()
    {
        _colorRandomizer.Restore();
        _isColorChanged = false;
    }
}