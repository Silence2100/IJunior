using UnityEngine;
using System;
using System.Collections;

public class Counter : MonoBehaviour
{
    private float _count = 0f;
    private WaitForSeconds _waitTime;
    private Coroutine _countingCoroutine;

    [SerializeField] private float _updateInterval = 0.5f;

    public event Action<float> OnCounterUpdated;

    private void Start()
    {
        _waitTime = new WaitForSeconds(_updateInterval);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_countingCoroutine != null)
            {
                StopCoroutine(_countingCoroutine);
                _countingCoroutine = null;
            }
            else
            {
                _countingCoroutine = StartCoroutine(StartCounting());
            }
        }
    }

    private IEnumerator StartCounting()
    {
        while (true)
        {
            _count++;
            OnCounterUpdated?.Invoke(_count);    

            yield return _waitTime;
        }
    }
}
