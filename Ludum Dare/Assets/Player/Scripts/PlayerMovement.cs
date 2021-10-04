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
        public int invertedControls = 1;
        public int invertedGun = 1;
        public Animator anim;
        public LayerMask ground;
        public GameObject dashCloud;
        public GameObject jumpCloud;

        #region Parameters
        public float moveSpeedCap => data.moveSpeedCap;

        public float moveSpeedAccelerationRate => data.moveSpeedAccelerationRate;
        public float dashSpeed => data.dashSpeed;
        float jumpVelocity => data.jumpVelocity;
        public float dashCooldown => data.dashCooldown;
        public float dashTime => data.dashTime;
        public float shootCooldown => data.shootCooldown;
        public float bulletSpeed => data.bulletSpeed;
        public float shootUnstability => data.shootUnstability;
        public float moveSmooth => data.moveSmooth;

        #endregion
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            facingDirection = 1;
            jumps = 2;
            if (GameManager.instance != null)
            {
                transform.position = GameManager.instance.checkpoint;
                Debug.Log("Yes;");
            }
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
        public Vector2 currentPoint;
        private void OnDrawGizmos()
        {
            
            Gizmos.DrawSphere(currentPoint, 0.5f);
        }
        void CheckGround()
        {
        }
        bool canShoot = true;
        void Shoot()
        {
            anim.SetTrigger("Shoot");
            GameObject _bulletPrefab = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            _bulletPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed * facingDirection * invertedGun, 0));
            _bulletPrefab.transform.localScale = new Vector3(0.5f * facingDirection, 0.5f, 1);
            Destroy(_bulletPrefab, 10);
            UnstabilityManager.instance.AddUnstability(shootUnstability);
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
            if (state == PlayerState.Dashing) return;
            if (GameManager.instance.state == GameManager.GameState.Pause) return;

            CheckGround();
            Move(Input.GetAxisRaw("Horizontal"));
            //else
            //{
            //    rb.velocity = new Vector2(0, rb.velocity.y);
            //}
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

            if (Input.GetKeyDown(KeyCode.C) && jumps >= 1)
            {
                
                Jump();
            }
        }
        public bool canDash = true;

        IEnumerator Dash()
        {
            
            
            PlayerState currentState = state;
            state = PlayerState.Dashing;
           
            rb.velocity = new Vector2(dashSpeed * facingDirection, 0);
            GameObject h = Instantiate(dashCloud, transform.position + new Vector3(1.4f * facingDirection, -0.58f, 0), Quaternion.identity);
            h.transform.localScale = new Vector3(h.transform.localScale.x * facingDirection, h.transform.localScale.y, h.transform.localScale.z);
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

        private Vector3 velocity = Vector3.zero;
        void Move(float direction)
        {
            int dir = Mathf.RoundToInt(direction);
            if (dir != 0)
            {
                if (state != PlayerState.Jumping)
                {
                    anim.SetInteger("PlayerState", 2);
                }
                facingDirection = dir * invertedControls;
            }
            else if (state != PlayerState.Jumping)
            {
                anim.SetInteger("PlayerState", 1);
            }
            Vector3 targetVelocity = new Vector2(moveSpeedCap * direction * invertedControls, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, moveSmooth);
            //rb.AddForce(new Vector2(moveSpeedAccelerationRate * direction * Time.deltaTime * invertedControls, 0));
            /*if (rb.velocity.magnitude > moveSpeedCap)
            {

                rb.velocity = new Vector2(moveSpeedCap * direction * invertedControls, rb.velocity.y);
            }*/
            transform.localScale = new Vector2(-facingDirection * Mathf.Abs(transform.localScale.x), transform.localScale.y);

        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 6 && collision.GetContact(0).point.y <= transform.position.y - 0.45f)
            {
                currentPoint = collision.GetContact(0).point;
                Debug.Log(collision.GetContact(0).point.x);
                state = PlayerState.Walking;
                jumps = 2;

            }
        }
        public int jumps;
        void Jump()
        {
            if (jumps == 1)
            {
                GameObject h = Instantiate(dashCloud, transform.position + new Vector3(0.15f * -facingDirection, -1f, 0), Quaternion.Euler(new Vector3(0, 0, 90)));
            }
            jumps--;
            
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpVelocity * (rb.gravityScale/Mathf.Abs(rb.gravityScale) )));
            state = PlayerState.Jumping;
            anim.SetInteger("PlayerState", 3);
        }
    }
}
