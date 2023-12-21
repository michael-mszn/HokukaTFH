using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDeathAnimation(GameObject mob)
    {
      mob.GetComponent<Animator>().SetInteger("AnimationState",1);   
    }

    public void PlayFreezeAnimation(GameObject mob)
    {
        mob.GetComponent<Animator>().SetInteger("AnimationState",2);  
    }
    
    public void PlayWalkAnimation(GameObject mob)
    {
        mob.GetComponent<Animator>().SetInteger("AnimationState",0);  
    }
}
