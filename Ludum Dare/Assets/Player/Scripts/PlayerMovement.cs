using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerData data;
        public Rigidbody2D rb;
        public Collider2D theCollider;
        public int facingDirection = 1;
        public GameObject bulletPrefab;


        #region Parameters
        public float moveSpeedCap => data.moveSpeedCap;

        public float moveSpeedAccelerationRate => data.moveSpeedAccelerationRate;
        public float dashSpeed => data.dashSpeed;
        float jumpVelocity => data.jumpVelocity;
        public float dashCooldown => data.dashCooldown;
        public float dashTime => data.dashTime;
        public float shootCooldown => data.shootCooldown;
        public float bulletSpeed => data.bulletSpeed;

        #endregion
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        public enum PlayerState
        {
            Walking,
            Jumping,
            Dashing,

            
        }
        public PlayerState state;
        void Start()
        {
            state = PlayerState.Walking;
            canDash = true;
            canShoot = true;
        }


        void Update()
        {
            HandleInput();
            
        }
        public Vector2 point;
        public float radius;
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(point + new Vector2(transform.position.x, transform.position.y), radius);
        }
        void CheckGround()
        {
            Collider2D colliders = Physics2D.OverlapCircle(point + new Vector2(transform.position.x, transform.position.y), radius);
            
            if (colliders != null)
            {
                if (colliders.CompareTag("Ground"))
                {
                    state = PlayerState.Walking;
                }
            }
           
        }
        bool canShoot = true;
        void Shoot()
        {
            GameObject _bulletPrefab = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            _bulletPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed * facingDirection, 0));
            StartCoroutine(ShootCooldown());
        }
        IEnumerator ShootCooldown()
        {
           
            canShoot = false;
            yield return new WaitForSeconds(shootCooldown);
            canShoot = true;
            
        }

        void HandleInput()
        {
            if (state != PlayerState.Dashing)
            {
                CheckGround();
                
                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (canDash)
                    {
                        StartCoroutine(Dash());
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Z) && canShoot == true)
                {
                    Shoot();
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Move(-1);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    Move(1);
                }

                else
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                if (state == PlayerState.Walking)
                {

                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        Jump();
                    }

                }
            }
           
        }
        public bool canDash = true; 
        IEnumerator Dash()
        {
            print("yes");
            PlayerState currentState = state;
            state = PlayerState.Dashing;
            rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
            yield return new WaitForSeconds(dashTime);
            StartCoroutine(DashCooldown());
            state = currentState;


        }        
        IEnumerator DashCooldown()
        {
            
            canDash = false;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }

        void Move(int direction)
        {
            facingDirection = direction;
            rb.AddForce(new Vector2(moveSpeedAccelerationRate * direction * Time.deltaTime, 0));
            if (rb.velocity.magnitude > moveSpeedCap)
            {
                
                rb.velocity = new Vector2(moveSpeedCap * direction, rb.velocity.y);
            }
            
        }
        void Jump()
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpVelocity));
            state = PlayerState.Jumping;
        }


       




    }
}
