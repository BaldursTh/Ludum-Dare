using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        public float speed;
        private Rigidbody2D rb;
        public bool useGravity = false;
        public bool home;
        public GameObject player;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");
            
            if (useGravity)
            {
                rb.gravityScale = 1;
                rb.AddForce(transform.right * speed);
                rb.constraints = RigidbodyConstraints2D.None;
                
            }
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            

            if (home)
            {
                
                Vector2 direction =( transform.position - player.transform.position).normalized;
                rb.velocity = -direction * speed;
            }
            else if (!useGravity)
                rb.velocity = transform.right * speed;
        }
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!(collision.CompareTag("Enemy Projectile") || collision.CompareTag("Enemy")))
                
                Destroy(gameObject);
        }
    }
}