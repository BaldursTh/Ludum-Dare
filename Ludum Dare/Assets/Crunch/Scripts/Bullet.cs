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
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            rb.velocity = transform.right * speed * Time.deltaTime;
        }
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!(collision.CompareTag("Enemy Projectile") || collision.CompareTag("Enemy")))
                Destroy(gameObject);
        }
    }
}