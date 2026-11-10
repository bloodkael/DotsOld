using DefaultNamespace.Components.Interfaces;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, IAbility
{
    public Collider newCollider;

    public List<MonoBehaviour> collisionActions = new List<MonoBehaviour>();

    [HideInInspector] public List<IAbilityTarget> collisionActionsAbilities = new List<IAbilityTarget>();

    [HideInInspector] public List<Collider> collisions;

    private void Start()
    {
        foreach (var action in collisionActions)
        {
            if (action is IAbilityTarget ability)
            {
                collisionActionsAbilities.Add(ability);
            } else
            {
                Debug.LogError("Collision actions must derive from IAbilityTarget!");
            }
        }
    }

    public void Execute()
    {
        foreach (var action in collisionActionsAbilities)
        {
            action.Targets = new List<GameObject>();
            collisions.ForEach(c =>
            {
                if (c != null) action.Targets.Add(c.gameObject);
            });
            action.Execute();
        }
    }


    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = gameObject.transform.position;
        switch (newCollider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    InitialTakeOff = true
                });
                break;
            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType= ColliderType.Capsule,
                    CapsuleStart = capsuleStart,
                    CapsuleEnd = capsuleEnd,
                    CapsuleRadius = capsuleRadius,
                    InitialTakeOff = true
                });
                break;
            case BoxCollider box:
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    InitialTakeOff = true
                });
                break;

        }

        newCollider.enabled = false;
    }
}

public struct ActorColliderData : IComponentData
{
    public ColliderType ColliderType;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtents;
    public quaternion BoxOrientation;
    public bool InitialTakeOff;
}

public enum ColliderType
{
    Sphere = 0,
    Capsule = 1,
    Box = 2
}
