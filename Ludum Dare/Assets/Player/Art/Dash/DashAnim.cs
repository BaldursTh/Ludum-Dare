using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAnim : MonoBehaviour
{
    public Animator anim;
    public float clipLength;
   

    void Start()
    {
        anim.Play("DashAnim");
        Destroy(gameObject, clipLength);
    }

    
}
