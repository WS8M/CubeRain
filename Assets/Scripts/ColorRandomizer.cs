using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorRandomizer : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Color _defaultColor;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
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