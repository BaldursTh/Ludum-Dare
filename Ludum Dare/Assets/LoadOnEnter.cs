using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadOnEnter : MonoBehaviour
{
    public GameObject LoadScreen;
    public Slider bar;

    public string Scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadSceneAsync());
        }
    }
    IEnumerator LoadSceneAsync()
    {
        LoadScreen.SetActive(true);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(Scene);
        while (!loadScene.isDone)
        {
            float progress = Mathf.Clamp01(loadScene.progress / 0.9f);

            bar.value = progress;
            yield return null;
        }
    }
}
