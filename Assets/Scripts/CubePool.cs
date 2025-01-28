using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _poolSize = 20;

    private ObjectPool<GameObject> _cubePool;

    private void Awake()
    {
        _cubePool = new ObjectPool<GameObject> (
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: cube => cube.SetActive(true),
            actionOnRelease: cube => cube.SetActive(false),
            actionOnDestroy: cube => Destroy(cube),
            maxSize: _poolSize
        );
    }

    public GameObject GetCube()
    {
        return _cubePool.Get();
    }

    public void ReturnCube(GameObject cube)
    {
        _cubePool.Release(cube);
    }
}