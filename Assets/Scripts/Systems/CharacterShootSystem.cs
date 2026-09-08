using DefaultNamespace.Components.Interfaces;
using Unity.Entities;

public class CharacterShootSystem : ComponentSystem
{
    private EntityQuery _shootQuery;

    protected override void OnCreate()
    {
        _shootQuery = GetEntityQuery(
            ComponentType.ReadOnly<InputData>(),
            ComponentType.ReadOnly<ShootData>(),
            ComponentType.ReadOnly<UserInputData>()
            );
    }
    protected override void OnUpdate()
    {
        Entities.With(_shootQuery).ForEach(
            (Entity entity, UserInputData userInputData, ref InputData inputData) =>
            {
                if (userInputData.ShootAction != null && userInputData.ShootAction is IAbility ability && inputData.Shoot > 0f)
                {
                    ability.Execute();
                }
            });
    }
}
