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
            SceneManager.LoadScene(SceneIndex);
        }
        // Update is called once per frame
        private void Update()
        {
            if(player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
            if(player.transform.position.y <= transform.position.y)
            {
                player.GetComponent<PlayerHealth>().LooseHealth();
                player.GetComponent<PlayerHealth>().LooseHealth();
                player.GetComponent<PlayerHealth>().LooseHealth();
                this.enabled = false;
            }
        }
    }
}