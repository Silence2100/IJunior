using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    [Header("Настройка новых кубов")]
    [SerializeField, Tooltip("Префаб куба, который будет создаваться")]
    private GameObject _cubePrefab;

    [SerializeField, Tooltip("Минимальное количество новых кубов")]
    [Range(1, 10)] private int _minCubes = 2;

    [SerializeField, Tooltip("Максимальное количество новых кубов")]
    [Range(1, 10)] private int _maxCubes = 6;

    [SerializeField, Tooltip("Множитель уменьшения размера нового куба")]
    [Range(0.1f, 1f)] private float _scaleMultiplier = 0.5f;

    [SerializeField, Tooltip("Радиус появления новых кубов вокруг исчезающего")]
    [Range(0.1f, 5f)] private float _spawnRadius = 0.5f;

    [Header("Настройка цвета")]
    [SerializeField, Tooltip("Включить случайные цвета для новых кубов")]
    private bool _randomizeColor = true;

    [Header("Шанс разделения")]
    [SerializeField, Tooltip("Начальный шанс разделения (в процентах)")]
    [Range(0f, 100f)] private float _initialSplitChance = 100f;

    [SerializeField, Tooltip("Снижение шанса разделения с каждым поколением")]
    [Range(0.1f, 2f)] private float _splitChanceDecay = 0.5f;

    [Header("Настройка взрыва")]
    [SerializeField, Tooltip("Сила взрыва")]
    [Range(0f, 50f)] private float _explosionForce = 10f;

    private float _currentSplitChance;

    private void Start()
    {
        _currentSplitChance = _initialSplitChance;
    }

    private void OnMouseDown()
    {
        Debug.Log($"Текущий шанс разделения: {_currentSplitChance}%");

        if (Random.value > _currentSplitChance / 100f)
        {
            Destroy(gameObject);
            return;
        }

        int cubeCount = Random.Range(_minCubes, _maxCubes + 1);

        for (int i = 0; i < cubeCount; i++)
        {
            Vector3 newPosition = transform.position + Random.insideUnitSphere;

            GameObject newCube = Instantiate(_cubePrefab, newPosition, Quaternion.identity);

            newCube.transform.localScale = transform.localScale * _scaleMultiplier;

            if (_randomizeColor)
            {
                Renderer renderer = newCube.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.material.color = GetRandomColor();
                }
            }

            ApplyExplosionForce(newCube);

            CubeClickHandler newCubeHandler = newCube.GetComponent<CubeClickHandler>();

            if (newCubeHandler != null)
            {
                newCubeHandler.SetSplitChance(_currentSplitChance * _splitChanceDecay);
            }
        }

        Destroy(gameObject);
    }

    private void ApplyExplosionForce(GameObject cube)
    {
        Rigidbody rb = cube.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 explosionDirection = (cube.transform.position - transform.position).normalized;
            rb.AddForce(explosionDirection * _explosionForce, ForceMode.Impulse);
        }
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    public void SetSplitChance(float chance)
    {
        _initialSplitChance = chance;
    }
}