using UnityEngine;

public class CubeSpawner : MonoBehaviour
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

    [SerializeField, Tooltip("Снижение шанса разделения с каждым поколением")]
    [Range(0.1f, 2f)] private float _splitChanceDecay = 0.5f;

    private ExplosionHandler _explosionHandler;

    private void Awake()
    {
        _explosionHandler = GetComponent<ExplosionHandler>();
    }

    public void SpawnCubes(Vector3 origin, Vector3 parentScale, float currentSplitChance)
    {
        int cubeCount = Random.Range(_minCubes, _maxCubes + 1);

        for (int i = 0; i < cubeCount; i++)
        {
            Vector3 spawnPosition = origin + Random.insideUnitSphere * _spawnRadius;

            GameObject newCube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
            newCube.transform.localScale = parentScale * _scaleMultiplier;

            if (_randomizeColor)
            {
                Renderer renderer = newCube.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = GetRandomColor();
                }
            }

            if (_explosionHandler != null)
            {
                _explosionHandler.ApplyExplosionForce(newCube, origin);
            }

            Cube cube = newCube.GetComponent<Cube>();

            if (cube != null)
            {
                cube.Initialize(currentSplitChance * _splitChanceDecay, this);
            }
        }
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
