using UnityEngine;
using TMPro;
using System.Collections;

public class Counter : MonoBehaviour
{
    private bool _isCounting = false;
    private float _count = 0f;
    public TextMeshProUGUI CounterText;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isCounting = !_isCounting;

            if (_isCounting)
            {
                StartCoroutine(StartCounting());
            }
            else
            {
                StopCoroutine(StartCounting());
            }
        }
    }

    private IEnumerator StartCounting()
    {
        while (_isCounting)
        {
            _count++;
            CounterText.text = "Ñ÷¸ò÷èê: " + _count.ToString();

            yield return new WaitForSeconds(0.5f);
        }
    }
}
