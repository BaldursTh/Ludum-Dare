using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBehaviour : MonoBehaviour
    {
        public int health = 5;
        public int deathUnstability;
        [SerializeField]
        private Transform wallCheck, floorCheck, groundCheck;
        private Rigidbody2D rb;
        private SpriteRenderer spr;
        public float moveSpeed;
        public bool fall;
        public LayerMask ground;
        private Animator anim;
        [SerializeField] private Collider2D[] disableOnDeath;
        
        public int facingDirection;
        public GameObject healthGain;
        public AudioSource aud2;
        
        // Start is called before the first frame update
        void Start()
        {
            facingDirection = 1;
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            //spr = GetComponentInChildren<SpriteRenderer>();
            //explode = GameObject.Find("Smoke").GetComponent<ParticleSystem>();
            //boom = GameObject.Find("Boom").GetComponent<AudioSource>();
        }

        private void Update()
        {

        }

        public void TakeDamage()
        {
            health--;
            if (health <= 0)
            {
                foreach (Collider2D i in disableOnDeath)
                {
                    i.enabled = false;
                }
                
                
                UnstabilityManager.instance.AddUnstability(deathUnstability);
                anim.SetTrigger("Ded");
                //deathTick -= Time.deltaTime;
                moveSpeed = 0;
                //if (deathTick <= 0)
                StartCoroutine("Ded");
                
            }

        }
        public bool ded;
        IEnumerator Ded()
        {
            ded = true;
            if(aud2 != null)
                aud2.Play();
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            yield return new WaitForSeconds(0.25f);
            Instantiate(healthGain, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(new Vector2(0, -1), 0.05f);
        }
        // Update is called once per frame
        void FixedUpdate()
        {

            bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, ground);
            if (Physics2D.OverlapCircle(wallCheck.position, 0.05f, ground) || (!Physics2D.OverlapCircle(floorCheck.position, 0.05f, ground) && !fall))
            {
                if (grounded)
                {
                    facingDirection *= -1;
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
            rb.velocity = new Vector2(moveSpeed * -facingDirection * Time.fixedDeltaTime * 50, rb.velocity.y);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player Projectile"))
            {
                TakeDamage();
            }
        }
    }
}