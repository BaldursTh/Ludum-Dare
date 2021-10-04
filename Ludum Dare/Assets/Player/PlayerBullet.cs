using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("HealthPickup") && !collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }
}
