using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour
{
  
    float speed = 15.0f;

    void Update()
    {
        Vector3 dir = Vector3.zero;


        dir.y = Input.acceleration.y;
        dir.x = Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();


        dir *= Time.deltaTime;


        transform.Translate(dir * speed);
    }

}
