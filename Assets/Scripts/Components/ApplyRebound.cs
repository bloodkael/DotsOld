using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRebound : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> Targets { get; set; }

    public void Execute()
    {
        bool needDestroy = false;
        foreach (var target in Targets)
        {
            var shootAbility = target.GetComponent<ShootAbility>();
            if (shootAbility && shootAbility.IsBulletRebound == false)
            {
                shootAbility.IsBulletRebound = true;
                needDestroy = true;
            }
        }
        if (needDestroy) Destroy(gameObject);
    }
}
