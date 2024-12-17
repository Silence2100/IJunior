using UnityEngine;

public class CubeMoveRotateGrow : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2.0f;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 50.0f;

    [Header("Growth Settings")]
    [SerializeField] private float growthSpeed = 0.5f;

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationAmount, 0f);

        float scaleChange = growthSpeed * Time.deltaTime;
        transform.localScale += new Vector3(scaleChange, scaleChange, scaleChange);
    }
}
