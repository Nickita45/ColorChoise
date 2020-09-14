using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLogo : MonoBehaviour
{
   

    // Update is called once per frame
    public GameObject logo;
    public GameObject menu;
    void Update()
    {
        if(!GetComponent<Animation>().isPlaying)
        {
            logo.SetActive(false);
            menu.SetActive(true);
        }
    }
}
