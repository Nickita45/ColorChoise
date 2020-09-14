using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Image fillmg;
    float timeAmt = 5;
    float time;
    public GameObject TimerObj, AfterTimerObj;
    // Start is called before the first frame update
    void Start()
    {
        fillmg = this.GetComponent<Image>();
        time = timeAmt;
    }
    public void setTimer(){
        time = timeAmt;
    }
    // Update is called once per frame
    void Update()
    {
        if (time > 0) {
            time -= Time.deltaTime;
            fillmg.fillAmount = time / timeAmt; 
        }
        else
        {
            AfterTimerObj.SetActive(true);
            TimerObj.SetActive(false);
            time = timeAmt;
        }
    }
}
