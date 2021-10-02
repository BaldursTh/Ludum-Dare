using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    
    public float sizeGrowSpeed;
    public float growthTime;
    float time;
    private void OnEnable()
    {
        time = Time.time;
        transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        while (Time.time - time <= growthTime)
        {
            transform.localScale += new Vector3(sizeGrowSpeed, sizeGrowSpeed, sizeGrowSpeed);
        }
    }
}
