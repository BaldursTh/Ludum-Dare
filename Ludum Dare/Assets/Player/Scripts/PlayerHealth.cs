using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public PlayerData data;
    public Slider greenSlider;
    public Slider whiteSlider;

    public float currenthealthGain;
    public float maxHealthGain = 100;
    public float currentHealth;

    public float maxHealth => data.maxHealth;
    public float healthGain => data.healthGain;
    public float invincibilityDuration => data.invincibilityDuration;


    public UnstableFeatures unstableFeatures;
    void Start()
    {
        currentHealth = maxHealth;
        currenthealthGain = 0;
        greenSlider.value = 0;
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 7)
        {
            LooseHealth();
           StartCoroutine(Invincibility());
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HealthPickup") && currentHealth != maxHealth)
        {
            currenthealthGain += healthGain;
            greenSlider.value = currenthealthGain / maxHealthGain;
            Destroy(collision.gameObject);
            if (currenthealthGain >= maxHealthGain)
            {
                
                currenthealthGain = 0;
                
                currentHealth += 1;
                greenSlider.value = 0;
                CheckHealth();
                
            }
        }
        if (collision.gameObject.layer == 7)
        {
            LooseHealth();
            StartCoroutine(Invincibility());
        }
    }
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    void LooseHealth()
    {
        currentHealth -= 1;
        CheckHealth();
       // unstableFeatures.FakeError(2, 0, 0, 0, "", "", gameObject);
       
    }
    IEnumerator Invincibility()
    {
        
        Physics2D.IgnoreLayerCollision(0, 7, true);
        yield return new WaitForSeconds(invincibilityDuration);
        Physics2D.IgnoreLayerCollision(0, 7, false);
        
    }
    void CheckHealth()
    {
        if (currentHealth == 3)
         {
             heart3.GetComponent<Image>().enabled = true;
             heart2.GetComponent<Image>().enabled = true;
             heart1.GetComponent<Image>().enabled = true;
         }
         if (currentHealth == 2)
         {
             heart3.GetComponent<Image>().enabled = true;
             heart2.GetComponent<Image>().enabled = true;
             heart1.GetComponent<Image>().enabled = false;
         }
         if (currentHealth == 1)
         {
             heart3.GetComponent<Image>().enabled = true;
             heart2.GetComponent<Image>().enabled = false;
             heart1.GetComponent<Image>().enabled = false;

         }

         if (currentHealth <= 0)
         {
             heart2.GetComponent<Image>().enabled = false;
             heart1.GetComponent<Image>().enabled = false;
             heart3.GetComponent<Image>().enabled = false;
             print("u lost");
         }
    }
}
