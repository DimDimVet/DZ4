using Unity.Entities;
using Unity.Mathematics;

public class UserAnimSystem : ComponentSystem
{
    private EntityQuery animQuery;//создадим переменую результата запроса сущностей
    protected override void OnCreate()
    {
        //получим результат запроса всех сущностей имеющие InputData и UserInputDataComponent
        animQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
                                   ComponentType.ReadOnly<UserInputDataComponent>());
    }
    protected override void OnUpdate()
    {
        //при каждом кадре ищем в сущностях изменеия структуры InputData
        Entities.With(animQuery).ForEach
            (
            (Entity entity, UserInputDataComponent userInput, ref InputData inputData) =>
            {
                if (userInput.CurrentAnim != null && userInput.CurrentAnim is IAnimComponent ability)
                {
                    //pull
                    //реакция на изменеия, запустим анимацию 
                    bool isPull;
                    if (inputData.Pull > 0f)
                    {
                        isPull = true;
                    }
                    else
                    {
                        isPull = false;
                    }
                    ability.GetPull(isPull);

                    //move
                    //реакция на изменеия, запустим анимацию
                    float2 move; 
                    if (inputData.Move.x != 0f | inputData.Move.y != 0f)
                    {
                        move = inputData.Move;
                    }
                    else
                    {
                        move.x = 0f;
                        move.y = 0f;
                    }
                    ability.GetMove(move);

                }
            }
            );
    }
}
