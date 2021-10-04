using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class Respawn : MonoBehaviour
    {
        private Transform player;
        public static Respawn instance = null;

        public int SceneIndex = 2;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                print("wtf");
                print(instance);
                Destroy(gameObject);
            }
        }

        public void RespawnPlayer()
        {
            SceneManager.LoadScene(2);
        }
        // Update is called once per frame
        void Update()
        {
            if (player.position.y <= transform.position.y)
            {
                SceneManager.LoadScene(SceneIndex);
            }
        }
    }
}