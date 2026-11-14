using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public Settings settings;
    public ShootAbility shootAbility;
    private int _health = int.MaxValue;
    
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            WriteStatistics();
            if (_health <= 0) Destroy(gameObject);
        }
    }

    private void WriteStatistics()
    {
        var jsonString = JsonUtility.ToJson(shootAbility.stats);
        Debug.Log(jsonString);
        PlayerPrefs.SetString("Stats", jsonString);
    }

    private void Start()
    {
        Health = settings.HeroHealth;
    }
}
