using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    float fireRate;
    float nextFire;
    public Transform position;
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            Instantiate(bullet, position.transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
