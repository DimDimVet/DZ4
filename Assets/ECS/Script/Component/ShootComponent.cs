using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour, IShootComponent
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform outBullet;
    public bool IsModeBull = false;
    //соберем в лист стороние скрипты
    public List<MonoBehaviour> CollisionAction = new List<MonoBehaviour>();

    public float ShootDelay;
    private float shootTime = float.MinValue;
    private BullComponent scrBullet;
    private void Start()
    {
        scrBullet = bullet.GetComponent<BullComponent>();
    }

    public void Shoot()
    {
        if (Time.time < shootTime + ShootDelay)
        {
            return;
        }
        else
        {
            shootTime = Time.time;
        }

        Instantiate(bullet, outBullet.position, outBullet.rotation);
        if (IsModeBull)
        {
            scrBullet.IsMode=true;
        }
        else
        {
            scrBullet.IsMode = false;
        }
        
       // currentBulletVelocity.AddForce(outBullet.up * bulletSpeed, ForceMode.Force);

    }
}
