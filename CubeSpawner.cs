using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 6;
    [SerializeField] private float _scaleMultiplier = 0.5f;
    [SerializeField] private float _spawnRadius = 0.5f;
    [SerializeField] private bool _randomizeColor = true;
    [SerializeField] private float _splitChanceDecay = 0.5f;

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
