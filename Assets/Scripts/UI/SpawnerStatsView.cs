using TMPro;
using UnityEngine;

public class SpawnerStatsView: MonoBehaviour
{
    [SerializeField] private string _spawnerName;
    [SerializeField] private SpawnerStats _spawnerStats;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable() => 
        _spawnerStats.Update += OnUpdate;

    private void OnDisable() => 
        _spawnerStats.Update -= OnUpdate;

    private void Start() => 
        _text.text = GetTextFromStats();

    private void OnUpdate() => 
        _text.text = GetTextFromStats();

    private string GetTextFromStats()
    {
        return $"{_spawnerName}\n" +
               $"Количество созданых - {_spawnerStats.NumberOfCreated}\n" +
               $"Количество заспавненых - {_spawnerStats.NumberOfSpawned}\n" +
               $"Количество Активных -{_spawnerStats.NumberOfActive}";
    }
}