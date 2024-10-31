using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System;

[Serializable]
public struct Range 
{
    [SerializeField, Range(0, 100), Tooltip("Excluded")] public int min;
    [SerializeField, Range(0, 100), Tooltip("Included")] public int max;

    public Range(int min, int max)
    {
        this.min = min;
        this.max = max;
    }
}

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image _healthBarBackground;
    [SerializeField] Image _healthBar;
    [SerializeField] Health _health;

    [SerializedDictionary("Range of health in %", "Color")] 
    public SerializedDictionary<Range, Color> colorBasedOnHp = new()
    {
        {new Range(0, 100), Color.green},
    };

    //[SerializedDictionary("Range of health in %", "Color")]
    //public SerializedDictionary<int, Color> colorBasedOnHp = new()
    //{
    //    {0, Color.green},
    //};

    private void Awake()
    {
        _health.OnHealthUpdate += UpdateHealthBar;
    }

    void UpdateHealthBar()
    {
        float healthPercentage = (float)_health.CurrentHealth / (float)_health.MaxHealth;
        _healthBar.fillAmount = healthPercentage;
        KeyValuePair<Range, Color> healthBarColor = colorBasedOnHp.FirstOrDefault(x => (x.Key.min / 100.0f) < healthPercentage && healthPercentage <= (x.Key.max / 100.0f));
        _healthBar.color = healthBarColor.Value;
    }

    //void UpdateHealthBar()
    //{
    //    float healthPercentage = (float)_health.CurrentHealth / (float)_health.MaxHealth;
    //    _healthBar.fillAmount = healthPercentage;
    //    Color healthBarColor = colorBasedOnHp.First(x => x.Key < healthPercentage && healthPercentage < x.Key).Value;
    //    _healthBar.color = healthBarColor;
    //}
}
