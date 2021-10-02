using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerData data;
    public float currenthealthMeter;
    public float maxHealthMeter;
    public float currentHealth;

    public float maxHealth => data.maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            currentHealth -= 1;
            if (currentHealth <= 0)
            {
                print("u lost");
            }
        }
    }
}
