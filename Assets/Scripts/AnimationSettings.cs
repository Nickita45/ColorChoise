using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSettings : MonoBehaviour
{
    // Start is called before the first frame update
   private bool isActive = false;
   private float default_time;
   private Animation anim; 
   void Start()
   {
       anim = this.GetComponent<Animation>();
       default_time=anim["settingsAnim"].time;
   }
   public void onClickSettings()
   {
       if(!isActive)
       {
           isActive=true;
           anim["settingsAnim"].time = default_time;
           anim["settingsAnim"].speed = 1;
           anim.Play();
       }
       else
       {
           isActive=false;
           anim["settingsAnim"].time = anim["settingsAnim"].length;
           anim["settingsAnim"].speed = -1;
           anim.Play();
       }
   }
}
