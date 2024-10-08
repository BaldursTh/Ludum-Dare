﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class Load : MonoBehaviour
    {
        public GameObject LoadScreen;
        public Slider bar;
        public void LoadLevel(int index)
        {
            if(index == 0)
            {
                GameManager.instance.checkpoint = new Vector3();
            }
            StartCoroutine(LoadSceneAsync(index));
        }
        IEnumerator LoadSceneAsync(int index)
        {
            LoadScreen.SetActive(true);
            AsyncOperation loadScene = SceneManager.LoadSceneAsync(index);
            while (!loadScene.isDone)
            {
                float progress = Mathf.Clamp01(loadScene.progress / 0.9f);
                
                bar.value = progress;
                yield return null;
            }
        }
    }
}
