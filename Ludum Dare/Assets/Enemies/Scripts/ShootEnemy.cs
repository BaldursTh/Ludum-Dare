using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyBehaviour))]
    public class ShootEnemy : MonoBehaviour
    {
        public GameObject bullet;
        [Min(0.5f)] public float waitTime = 2f;
        public float bulletSpeed;
        public ShootType type;
        public bool useGravity = false;

        private EnemyBehaviour baseBehave;
        private float cacheSpeed;
        private Transform player;
        public bool home;
        public Animator anim;
        public float shootWait;

        public int bulType;

        private Vector3 spawnPos;
        public Transform spawnObj;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            baseBehave = GetComponent<EnemyBehaviour>();
            cacheSpeed = baseBehave.moveSpeed;
            StartCoroutine(Shoot());
        }

        IEnumerator Shoot()
        {
            while(baseBehave.health > 0)
            {
                

               yield return new WaitForSeconds(waitTime);
                Vector3 target = player.position;
                baseBehave.moveSpeed = 0;
                target.z = 0;
                target.x = target.x - transform.position.x;
                target.y = target.y - transform.position.y;
                if (useGravity)
                {
                    target.y += 2;
                }
                float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg; //calculate z anlge to face

                if (target.x <= 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (target.x > 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                anim.SetTrigger("Shoot");

                yield return new WaitForSeconds(shootWait);

                transform.localScale = new Vector3(baseBehave.facingDirection, transform.localScale.y);
                spawnPos = transform.position;
            if(spawnObj != null)
            {
                spawnPos = spawnObj.position;
            }
                switch (type)
                {
                    case ShootType.SingleTarget:
                        SingleTarget(angle);
                        break;
                    case ShootType.TripleTarget:
                        TripleTarget(angle);
                        break;
                    case ShootType.EightWay:
                        EightWay();
                        break;
                    default:
                        SingleTarget(angle);
                        break;
                }
                
                yield return new WaitForSeconds(0.3f);

                baseBehave.moveSpeed = cacheSpeed;
                transform.localScale = new Vector3(baseBehave.facingDirection, transform.localScale.y);

            }
        }

        void SingleTarget(float TargetAngle)
        {
            Bullet bul = Instantiate(bullet, spawnPos, Quaternion.Euler(new Vector3(0, 0, TargetAngle))).GetComponent<Bullet>();
            bul.useGravity = useGravity;
            bul.speed = bulletSpeed;
            bul.home = home;
            bul.type = bulType;
        }

        void TripleTarget(float TargetAngle)
        {
            for (int i = -1; i < 2; i++)
            {
                Bullet bul = Instantiate(bullet, spawnPos, Quaternion.Euler(new Vector3(0, 0, TargetAngle + 30 * i))).GetComponent<Bullet>();
                bul.useGravity = useGravity;
                bul.speed = bulletSpeed;
                bul.home = home;
                bul.type = bulType;
            }
        }

        void EightWay()
        {
            for(int i = 0; i < 8; i++)
            {
                Bullet bul = Instantiate(bullet, spawnPos, Quaternion.Euler(new Vector3(0, 0, 45 * i))).GetComponent<Bullet>();
                bul.useGravity = useGravity;
                bul.speed = bulletSpeed;
                bul.home = home;
                bul.type = bulType;
            }
        }
    }
    public enum ShootType
    {
        SingleTarget,
        TripleTarget,
        EightWay,
    }
}