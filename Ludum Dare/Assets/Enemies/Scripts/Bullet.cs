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
        public int type;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (useGravity)
            {
                rb.gravityScale = 1;
                rb.AddForce(transform.right * speed);
                rb.constraints = RigidbodyConstraints2D.None;
            }

            player = GameObject.FindGameObjectWithTag("Player");

            StartCoroutine(Delete());

            GetComponent<Animator>().SetInteger("Type", type);
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
        private void OnBecameVisible()
        {
            StopCoroutine(Delete());
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
        IEnumerator Delete()
        {
            yield return new WaitForSeconds(1f);
            var campos = Camera.main.transform.position;
            Debug.LogWarning(Vector3.Distance(new Vector3(transform.position.x, transform.position.y), new Vector3(campos.x, campos.y)));
            if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y), new Vector3(campos.x, campos.y)) >= 10f) 
            Destroy(gameObject);    
        }
    }
}