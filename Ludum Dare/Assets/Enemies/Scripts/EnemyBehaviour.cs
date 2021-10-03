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
        //private Animator anim;
        [SerializeField] private Collider2D[] disableOnDeath;
        //private float deathTick = 1.5f;
        public int facingDirection;
        public GameObject healthGain;
        //public ParticleSystem explode;
        //public AudioSource boom;
        // Start is called before the first frame update
        void Start()
        {
            facingDirection = 1;
            rb = GetComponent<Rigidbody2D>();
            //anim = GetComponent<Animator>();
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
                GetComponent<SpriteRenderer>().enabled = false;
                Instantiate(healthGain, transform.position, Quaternion.identity);
                UnstabilityManager.instance.AddUnstability(deathUnstability);
                //anim.SetBool("Dead", true);
                //deathTick -= Time.deltaTime;
                moveSpeed = 0;
                //if (deathTick <= 0)
                Destroy(gameObject);
            }

        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(new Vector2(0, -1), 0.05f);
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (Physics2D.OverlapCircle(new Vector2(0, -1), 0.05f, ground))
            {
                fall = true;
            }
            else
            {
                fall = false;
            }
            if (Physics2D.OverlapCircle(wallCheck.position, 0.05f, ground) || (!Physics2D.OverlapCircle(floorCheck.position, 0.05f, ground) && !fall))
            {
                facingDirection *= -1;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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