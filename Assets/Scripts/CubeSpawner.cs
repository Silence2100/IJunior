using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private float _spawnInterval = 0.2f;

    private float _minLifeTime = 2f;
    private float _maxLifeTime = 5f;

    private float _multiplierSpawnCubes = 2f;
    private float _heightSpawnCubes = 30f;

    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnCube();
            _timer = 0f;
        }
    }

    private void SpawnCube()
    {
        GameObject cubeObject = _cubePool.GetCube();
        Cube cube = cubeObject.GetComponent<Cube>();

        cube.OnTouchedPlatform += HandleCubeTouch;

        Vector3 spawnPosition = new Vector3(
            Random.Range(_spawnArea.position.x - _spawnArea.localScale.x * _multiplierSpawnCubes, _spawnArea.position.x + _spawnArea.localScale.x * _multiplierSpawnCubes),
            _spawnArea.position.y + _heightSpawnCubes,
            Random.Range(_spawnArea.position.z - _spawnArea.localScale.z * _multiplierSpawnCubes, _spawnArea.position.z + _spawnArea.localScale.z * _multiplierSpawnCubes)
        );

        cubeObject.transform.position = spawnPosition;
        cubeObject.transform.rotation = Quaternion.identity;

        cube.ResetCube();
    }

    private void HandleCubeTouch(Cube cube)
    {
        float lifeTime = Random.Range(_minLifeTime, _maxLifeTime + 1);
        StartCoroutine(DestroyCubeAfterDelay(cube, lifeTime));
    }

    private IEnumerator DestroyCubeAfterDelay(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);

        cube.OnTouchedPlatform -= HandleCubeTouch;
        _cubePool.ReturnCube(cube.gameObject);
    }
}
