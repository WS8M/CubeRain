using UnityEngine;

public class ColorRandomizer
{
    private MeshRenderer _meshRenderer;
    private Color _defaultColor;

    public  ColorRandomizer(MeshRenderer meshRenderer)
    {
        _meshRenderer = meshRenderer;
        _defaultColor = _meshRenderer.material.color;
    }

    public void Set() => _meshRenderer.material.color = GetRandomColor();

    public void Restore() => _meshRenderer.material.color = _defaultColor;

    private Color GetRandomColor()
    {
        var red = Random.Range(0f, 1f);
        var green = Random.Range(0f, 1f);
        var blue = Random.Range(0f, 1f);

        return new Color(red, green, blue, 1f);
    }
}