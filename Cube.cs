using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _currentSplitChance;
    private CubeSpawner _spawner;

    [SerializeField, Tooltip("Начальный шанс разделения для этого куба (в процентах)")]
    [Range(0f, 100f)] private float _initialSplitChance = 100f;

    private void Start()
    {
        if (_currentSplitChance == 0)
        {
            _currentSplitChance = _initialSplitChance;
        }

        _spawner = FindObjectOfType<CubeSpawner>();
    }

    private void OnMouseDown()
    {
        Debug.Log($"Текущий шанс разделения: {_currentSplitChance}%");

        if (Random.value > _currentSplitChance / 100f)
        {
            Destroy(gameObject);
            return;
        }

        if (_spawner != null)
        {
            _spawner.SpawnCubes(transform.position, transform.localScale, _currentSplitChance);
        }

        Destroy(gameObject);
    }

    public void Initialize(float initialSplitChance, CubeSpawner spawner)
    {
        _currentSplitChance = initialSplitChance;
        _spawner = spawner;
    }
}
