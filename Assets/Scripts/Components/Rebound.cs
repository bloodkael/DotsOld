using DefaultNamespace.Components.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : MonoBehaviour, IAbilityTarget
{
    public List<GameObject> Targets { get; set; }

    public void Execute()
    {
        Collider curCollider = gameObject.GetComponent<Collider>();
        if (!curCollider)
        {
            return;
        }

        BulletMovement movement = gameObject.GetComponent<BulletMovement>();
        if (movement == null) return;
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        if (rigidbody == null) return;
        foreach (var target in Targets)
        {
            Collider otherCollider = target.GetComponent<Collider>();

            if (curCollider == otherCollider) continue;

            Vector3 closestPoint = otherCollider.ClosestPoint(transform.position);
            Vector3 normal = (transform.position - closestPoint).normalized;
            Vector3 currentVelocity = transform.forward * movement.speed; // Ваша текущая скорость
            Vector3 bounceDirection = Vector3.Reflect(currentVelocity, normal);

            rigidbody.velocity = bounceDirection;
        }
    }
}
