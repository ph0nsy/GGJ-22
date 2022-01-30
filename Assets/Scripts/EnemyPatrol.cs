using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 5.0f;
    public float distance = 2.0f;
    private bool moving_right = false;
    public Transform groundCheck;
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInf = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);

        if (groundInf.collider == false)
        {
            if(moving_right == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moving_right = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                {
                    moving_right = true;
                }
            }
        }
    }
}
