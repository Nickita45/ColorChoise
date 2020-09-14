using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    [Range(0f,0.5f)] float lerpTime;
    [SerializeField]
    Vector3[] myangles;
    int anglexindex;
    int len;
    float t = 0f;
    void Start()
    {
        
        len = myangles.Length;
        
    }
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(myangles[anglexindex]),lerpTime*Time.deltaTime);
        t = Mathf.Lerp (t,1f,lerpTime*Time.deltaTime);
        if(t > .9f){
            t=0f;
            anglexindex = Random.Range(0,len-1);
        }

    }
}
