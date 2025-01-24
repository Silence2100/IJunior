using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private List<Cube> _listCubes;

    private CubeExplosion _explosion;

    private int _minCubes = 2;
    private int _maxCubes = 6;
    private float _scaleFactor = 0.5f;
    private float _cubeSpawnRadiusMultiplier = 0.1f;

    private void Awake()
    {
        _explosion = GetComponent<CubeExplosion>();
    }

    private void Start()
    {
        foreach (var cube in _listCubes)
            cube.OnClicked += OnCubeDestroyed;
    }

    public void SpawnCubes(Cube cube)
    {
        Debug.Log("Шанс деления: " + cube.CurrentChance);

        int newCubesCount = Random.Range(_minCubes, _maxCubes + 1);

        if (Random.value < cube.CurrentChance)
        {
            for (int i = 0; i < newCubesCount; i++)
            {
                Vector3 spawnPosition = cube.transform.position + Random.insideUnitSphere * _cubeSpawnRadiusMultiplier;

                Cube newCube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);

                newCube.transform.localScale = cube.transform.localScale * _scaleFactor;

                newCube.ChangeColor();

                newCube.ReduceChance(cube.CurrentChance);

                _explosion.ApplyExplosionForce(newCube);

                newCube.OnClicked += OnCubeDestroyed;
            }
        }
        else
        {
            Destroy(cube);
        }
    }

    private void OnCubeDestroyed(Cube cube)
    {
        SpawnCubes(cube);
        cube.OnClicked -= OnCubeDestroyed;
    }
}
