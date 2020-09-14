using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollViewScriot : MonoBehaviour
{
    public int numberToCreate;
    
    public void summonObj(GameObject gm) 
    {
        Instantiate(gm, transform);
    }
    public void Populate(List<GameObject> gameobjects)
    {
        for(int i=0; i<gameobjects.Count;i++)
        {
            Instantiate(gameobjects[i], transform);
        }
    }
}
