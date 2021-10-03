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

        public LayerMask ground;

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

        #endregion
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            facingDirection = 1;
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
        public Vector2 boxDimensions;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawCube(point + new Vector2(transform.position.x, transform.position.y), new Vector3(0.9f, 0.2f, 1f));
        }
        void CheckGround()
        {
            if (Physics2D.OverlapBox(point + new Vector2(transform.position.x, transform.position.y), new Vector2(0.9f, 0.2f), ground))
                state = PlayerState.Walking;
        }
        bool canShoot = true;
        void Shoot()
        {
            
            GameObject _bulletPrefab = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            _bulletPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed * facingDirection * invertedGun, 0));
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

            CheckGround();

            if (Input.GetKey(KeyCode.RightArrow))
            {
                Move(1);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move(-1);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
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

            if (state == PlayerState.Walking && Input.GetKeyDown(KeyCode.C))
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

        void Move(float direction)
        {
            facingDirection = Mathf.RoundToInt(direction) * invertedControls;
            rb.AddForce(new Vector2(moveSpeedAccelerationRate * direction * Time.deltaTime * invertedControls, 0));
            if (rb.velocity.magnitude > moveSpeedCap)
            {

                rb.velocity = new Vector2(moveSpeedCap * direction * invertedControls, rb.velocity.y);
            }
            transform.localScale = new Vector2(-facingDirection, transform.localScale.y);

        }
        void Jump()
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpVelocity * (rb.gravityScale/Mathf.Abs(rb.gravityScale) )));
            state = PlayerState.Jumping;
        }
    }
}
