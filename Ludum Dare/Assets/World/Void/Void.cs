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
        if (Time.time - time <= growthTime)
        {
            transform.localScale += new Vector3(sizeGrowSpeed * Time.deltaTime, sizeGrowSpeed * Time.deltaTime, sizeGrowSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6)
        {
            Destroy(collision.gameObject);
        }
    }
}
