using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _poolSize = 20;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject cube = Instantiate(_cubePrefab);
            cube.SetActive(false);
            _pool.Enqueue(cube);
        }
    }

    public GameObject GetCube()
    {
        if (_pool.Count > 0)
        {
            GameObject cube = _pool.Dequeue();
            cube.SetActive(true);
            return cube;
        }
        else
        {
            GameObject cube = Instantiate(_cubePrefab);
            return cube;
        }
    }

    public void ReturnCube(GameObject cube)
    {
        cube.SetActive(false);
        _pool.Enqueue(cube);
    }
}