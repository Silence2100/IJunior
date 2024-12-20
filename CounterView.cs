using UnityEngine;
using TMPro;

public class CounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterText;
    [SerializeField] private Counter _counter;

    private void OnEnable()
    {
        if (_counter != null)
        {
            _counter.OnCounterUpdated += UpdateCounterDisplay;
        }
    }

    private void OnDisable()
    {
        if (_counter != null)
        {
            _counter.OnCounterUpdated -= UpdateCounterDisplay;
        }
    }

    private void UpdateCounterDisplay(float count)
    {
        _counterText.text = "Ñ÷¸ò÷èê: " + count.ToString();
    }
}
