using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinsAroud : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isPyramind = false;
    // Update is called once per frame
    void Update()
    {

       // this.gameObject.transform.Rotate (Vector3.forward * 50 * Time.deltaTime, Space.World);
                if(isPyramind)
                    transform.Rotate( new Vector3(0,0,  0.3f) );
                else
                    transform.Rotate( new Vector3(0,0.3f,0) );
        
    }
}
