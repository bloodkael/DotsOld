using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> Targets { get; set; }

    public float damageDelay = .1f;

    public int Damage = 10;

    private float _damageTime = float.MinValue;

    public void Execute()
    {
        if (Time.time < _damageTime + damageDelay)
        {
            return;
        }
        _damageTime = Time.time;
        foreach (var target in Targets)
        {
            var health = target.GetComponent<CharacterHealth>();
            if (health != null)
            {
                health.Health -= Damage;
            }
        }
    }
}
