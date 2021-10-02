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
        [Min(200f)] public float bulletSpeed = 400f;
        public ShootType type;
        public bool useGravity = false;

        private EnemyBehaviour baseBehave;
        private float cacheSpeed;
        private Transform player;
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
                baseBehave.moveSpeed = 0;
                yield return new WaitForSeconds(0.5f);

                Vector3 target = player.position;
                target.z = 0;
                target.x = target.x - transform.position.x;
                target.y = target.y - transform.position.y;
                if (useGravity)
                {
                    target.y += 2;
                }
                float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg; //calculate z anlge to face

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
            }
        }

        void SingleTarget(float TargetAngle)
        {
            Bullet bul = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, TargetAngle))).GetComponent<Bullet>();
            bul.useGravity = useGravity;
            bul.speed = bulletSpeed;
        }

        void TripleTarget(float TargetAngle)
        {
            for (int i = -1; i < 2; i++)
            {
                Bullet bul = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, TargetAngle + 30 * i))).GetComponent<Bullet>();
                bul.useGravity = useGravity;
                bul.speed = bulletSpeed;
            }
        }

        void EightWay()
        {
            for(int i = 0; i < 8; i++)
            {
                Bullet bul = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 45 * i))).GetComponent<Bullet>();
                bul.useGravity = useGravity;
                bul.speed = bulletSpeed;
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