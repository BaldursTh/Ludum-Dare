using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public PlayerData data;
    public Slider greenSlider;
    public Slider whiteSlider;
    public Animator anim;

    public float currenthealthGain;
    public float maxHealthGain = 100;
    public float currentHealth;

    public GameObject gameOverScreen;
    public AudioSource aud;

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
        if (collision.CompareTag("Void"))
        {
            Death();
        }
    }

    public void Death()
    {
        GetComponent<Player.PlayerMovement>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        anim.SetInteger("PlayerState", 0);
        anim.SetTrigger("Death");
        gameOverScreen.SetActive(true);
        
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
        CheckHealth();
        GetComponent<Player.PlayerMovement>().enabled = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = true;
        anim.Rebind();
        anim.Play("PlayerIdle", -1, 0f);
        gameOverScreen.SetActive(false);
    }


    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public void LooseHealth()
    {
        aud.Play();
        currentHealth -= 1;
        CheckHealth();
       // unstableFeatures.FakeError(1, 0, 0, 0, "", "", gameObject);
       
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
            Death();
         }
    }
}
