using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyGlitchHandler : MonoBehaviour
    {
        private GameObject enemyHandler;
        public List<GameObject> enemies;
        public List<GameObject> bigEnemies;
        private GameObject cam;

        public bool spawn;
        public int count = 5;
        public float bigThreshold = 0.7f;

        // Start is called before the first frame update
        void Start()
        {
            enemyHandler = GameObject.FindGameObjectWithTag("Enemy Container");
            cam = Camera.main.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (spawn)
            {
                ResizeEnemies(0.5f);//count, bigThreshold);
                spawn = false;
            }
        }

        public void SpawnEnemies(int count, float bigThreshold)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject toSpawn;
                if (Random.Range(0f, 1f) >= bigThreshold)
                    toSpawn = bigEnemies[Random.Range(0, bigEnemies.Count)];
                else
                    toSpawn = enemies[Random.Range(0, enemies.Count)];
                Instantiate(toSpawn,
                    cam.transform.position + new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-3.5f, 3.5f), 10),
                    Quaternion.identity,
                    enemyHandler.transform);
            }
        }

        public void ScrambleEnemies()
        {
            List<Vector3> savedPos = new List<Vector3>();
            foreach(Transform i in enemyHandler.transform)
            {
                savedPos.Add(i.position);
            }
            foreach (Transform i in enemyHandler.transform)
            {
                int ind = Random.Range(0, savedPos.Count);
                i.position = savedPos[ind];
                savedPos.RemoveAt(ind);
            }
        }

        public void ResizeEnemies(float range)
        {
            foreach (Transform i in enemyHandler.transform)
            {
                i.localScale += new Vector3(Random.Range(-range, range) * Mathf.Sign(i.localScale.x), Random.Range(-range, range), 0);
                i.localScale = new Vector3(Mathf.Sign(i.localScale.x) * Mathf.Clamp(Mathf.Abs(i.localScale.x), 0.2f, 5f), Mathf.Clamp(i.localScale.y, 0.2f, 5f));
            }
        }
    }
}