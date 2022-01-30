using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    float fireRate;
    float nextFire;
    public Transform[] position;
    void Update()
    {
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        if(Time.time > nextFire)
        {
            for (int i = 0; i<position.Length; i++)
            {
                Instantiate(bullet, position[i].transform.position, Quaternion.identity);
            }
            nextFire = Time.time + fireRate;
        }
    }
}
