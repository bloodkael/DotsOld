using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyHealth : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> Targets { get; set; }

    public int Health = 10;

    public int MaxHealth = 100;

    public void Execute()
    {
        bool needDestroy = false;
        foreach (var target in Targets)
        {
            var health = target.GetComponent<CharacterHealth>();
            if (health != null && health.Health != MaxHealth)
            {
                int resultHealth = health.Health + Health;
                health.Health = Mathf.Clamp(resultHealth, 0, MaxHealth);
                needDestroy = true;
            }
        }
        if (needDestroy)
        {
            Destroy(gameObject);
        }
    }
}
