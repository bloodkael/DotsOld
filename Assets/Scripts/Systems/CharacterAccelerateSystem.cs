using Unity.Entities;

public class CharacterAccelerateSystem : ComponentSystem
{
    private EntityQuery _accelerateQuery;

    protected override void OnCreate()
    {
        _accelerateQuery = GetEntityQuery(
            ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadWrite<MoveData>(),
            ComponentType.ReadWrite<AccelerateData>(),
            ComponentType.ReadOnly<UserInputData>()
            );
    }
    protected override void OnUpdate()
    {
        Entities.With(_accelerateQuery).ForEach(
            (Entity entity, UserInputData userInputData, ref InputData inputData, ref MoveData moveData, ref AccelerateData accelerateData) =>
            {
                if (userInputData.AccelerateAction != null
                    && userInputData.AccelerateAction is AccelerateAbility ability
                    && accelerateData.Acceleration > 0f)
                {
                    if (inputData.Accelerate > 0f)
                    {
                        ability.Execute();
                    }
                    if (ability.IsAccelerated)
                    {
                        moveData.Speed = moveData.BaseSpeed * accelerateData.Acceleration;
                    } else
                    {
                        moveData.Speed = moveData.BaseSpeed;
                    }
                }
            });
    }
}
