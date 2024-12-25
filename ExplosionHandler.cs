using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [Header("Настройки взрыва")]
    [SerializeField, Tooltip("Сила взрыва")]
    private float _explosionForce = 10f;

    public void ApplyExplosionForce(GameObject cube, Vector3 explosionOrigin)
    {
        Rigidbody rb = cube.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (cube.transform.position - explosionOrigin).normalized;
            rb.AddForce(direction * _explosionForce, ForceMode.Impulse);
        }
    }
}