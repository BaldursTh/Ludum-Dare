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
                float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg; //calculate z anlge to face

                Bullet bul = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<Bullet>();
                bul.speed = bulletSpeed;
                yield return new WaitForSeconds(0.3f);
                baseBehave.moveSpeed = cacheSpeed;
            }
        }
    }
}