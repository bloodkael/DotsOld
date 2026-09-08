using DefaultNamespace.Components.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float speed;
    public float rotationSpeed;
    public float acceleration;

    public MonoBehaviour ShootAction;
    public MonoBehaviour AccelerateAction;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new MoveData { Speed = speed / 100, BaseSpeed = speed / 100, RotationSpeed = rotationSpeed });
        if (ShootAction != null && ShootAction is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }
        if (AccelerateAction != null && AccelerateAction is IAbility)
        {
            dstManager.AddComponentData(entity, new AccelerateData { Acceleration = acceleration });
        }
    }
}

public struct InputData: IComponentData
{
    public float2 Move;
    public float Shoot;
    public float Accelerate;
}

public struct MoveData: IComponentData
{
    public float Speed;
    public float BaseSpeed;
    public float RotationSpeed;
}

public struct AccelerateData : IComponentData
{
    public float Acceleration;
}

public struct ShootData: IComponentData
{

}
