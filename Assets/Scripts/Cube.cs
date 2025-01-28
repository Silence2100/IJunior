using System;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    public event Action<Cube> OnTouchedPlatform;

    private bool _hasTouched = false;
    private Color _originalColor;

    public Renderer Renderer { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Renderer = GetComponent<Renderer>();
        _originalColor = Renderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasTouched && collision.collider.CompareTag("Platform"))
        {
            _hasTouched = true;
            ChangeColor();

            OnTouchedPlatform?.Invoke(this);
        }
    }

    private void ChangeColor()
    {
        Renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    public void ResetCube()
    {
        _hasTouched = false;
        Renderer.material.color = _originalColor;
    }
}
