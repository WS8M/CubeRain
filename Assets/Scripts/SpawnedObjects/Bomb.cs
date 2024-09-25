using UnityEngine;

public class Bomb : TemporaryObject
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Explosion _explosion;
    
    private Color _defaultColor;
    
    private void OnEnable()
    {
        TimeElapsed += SetAlfa;
    }

    private void OnDisable()
    {
        TimeElapsed -= SetAlfa;
    }
    
    public void Get()
    {
        _defaultColor = _meshRenderer.material.color;
        StartLifeTime();
    }
    
    protected override void OnRemoved()
    {
        _meshRenderer.material.color = _defaultColor;
        _explosion.Activate();
    }

    private void SetAlfa(float progress)
    {
        Color newColor = new Color(_defaultColor.r, _defaultColor.g, _defaultColor.b, progress);
        _meshRenderer.material.color = newColor;
    }
}