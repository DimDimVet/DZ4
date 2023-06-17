using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public class UserInputDataComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField]private float speed;
    public MonoBehaviour ShootAction;
    public MonoBehaviour PullAction;
    public MonoBehaviour CurrentAnim;
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        entityManager.AddComponentData(entity, new InputData());//добавим в сущность стурктуру ввода
        entityManager.AddComponentData(entity, new MoveData()//добавим в сущность стурктуру режима движения(скорость)
        {
            MoveSpeed = speed / 100
        });
        if (ShootAction != null & ShootAction is IShootComponent)
        {
            entityManager.AddComponentData(entity, new ShootData());//добавим в сущность стурктуру ввода стрельбы
        }

        if (PullAction != null & PullAction is IPullComponent)
        {
            entityManager.AddComponentData(entity, new PullData());//добавим в сущность стурктуру ввода прыжка
        }

        if (CurrentAnim != null & CurrentAnim is IAnimComponent)
        {
            entityManager.AddComponentData(entity, new AnimData());//добавим в сущность стурктуру ввода анимации
        }
    }
}

//srukture Entity
public struct InputData : IComponentData
{
    public float2 Move;
    public float Shoot;
    public float Pull;
}
public struct MoveData : IComponentData
{
    public float MoveSpeed;
}

public struct ShootData : IComponentData
{
    //
}
public struct PullData : IComponentData
{
    //
}
public struct AnimData : IComponentData
{
    //
}
