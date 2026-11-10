using DefaultNamespace.Components.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionSystem : ComponentSystem
{
    private EntityQuery _collisionQuery;

    private Collider[] _results = new Collider[50];

    protected override void OnCreate()
    {
        _collisionQuery = GetEntityQuery(
            ComponentType.ReadOnly<ActorColliderData>(),
            ComponentType.ReadOnly<Transform>()
        );
    }

    protected override void OnUpdate()
    {
        var dstManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Entities.With(_collisionQuery).ForEach(
            (Entity entity, CollisionAbility collisionAbility, ref ActorColliderData actorColliderData) =>
            {
                if (collisionAbility == null) return;

                var gameObject = collisionAbility.gameObject;
                float3 position = gameObject.transform.position; 
                Quaternion rotation = gameObject.transform.rotation;

                collisionAbility.collisions = new List<Collider>();

                int size = 0;

                switch (actorColliderData.ColliderType)
                {
                    case ColliderType.Sphere:
                        size = Physics.OverlapSphereNonAlloc(actorColliderData.SphereCenter + position, actorColliderData.SphereRadius, _results);
                        break;
                    case ColliderType.Capsule:
                        var center = ((actorColliderData.CapsuleStart + position) + (actorColliderData.CapsuleEnd + position)) / 2f;
                        var point1 = actorColliderData.CapsuleStart + position;
                        var point2 = actorColliderData.CapsuleEnd + position;
                        point1 = (float3)(rotation * (point1 - center)) + center;
                        point2 = (float3)(rotation * (point2 - center)) + center;
                        size = Physics.OverlapCapsuleNonAlloc(point1, point2, actorColliderData.CapsuleRadius, _results);
                        break;
                    case ColliderType.Box:
                        size = Physics.OverlapBoxNonAlloc(actorColliderData.BoxCenter + position, actorColliderData.BoxHalfExtents, _results, actorColliderData.BoxOrientation * rotation);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (size > 0)
                {
                    foreach (var result in _results)
                    {
                        collisionAbility.collisions.Add(result);
                    }
                    collisionAbility.Execute();
                }
            }
        );
    }
}
