using Unity.Entities;
using UnityEngine;

[UpdateAfter(typeof(CharacterAccelerateSystem))]
public class CharacterMoveSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        _moveQuery = GetEntityQuery(
            ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<MoveData>(),
            ComponentType.ReadOnly<Transform>()
            );
    }
    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach(
            (Entity entity, Transform transform, ref InputData inputData, ref MoveData moveData) =>
            {
                Vector3 moveDirection = new Vector3(inputData.Move.x, 0, inputData.Move.y);
                var pos = transform.position;
                pos += moveDirection * moveData.Speed;
                transform.position = pos;
                Debug.Log(moveData.Speed);

                if (moveDirection.sqrMagnitude > .001f)
                {
                    Quaternion rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, moveData.RotationSpeed * Time.DeltaTime);
                }
            });
    }
}
