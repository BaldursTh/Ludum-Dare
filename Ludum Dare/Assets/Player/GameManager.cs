using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Play, Pause, Dead, Destroy
    }
    public GameState state;
    public GameObject pause;
    public static GameManager instance;

    public Vector3 checkpoint = new Vector3();
    public Vector3 start;

    private string scene;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            print("wtf");
            print(instance);
            Destroy(gameObject);
            instance.GetComponent<UnstableFeatures>().Reload();
            instance.GetComponent<UnstabilityManager>().Reload();
            instance.pause = GameObject.FindGameObjectWithTag("Pause");
            instance.state = GameState.Play;
        }
        pause.SetActive(false);
        state = GameState.Play;
        CheckState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == GameState.Play)
            {
                state = GameState.Pause;
            }
            else if (state == GameState.Pause)
            {
                state = GameState.Play;
            }
            CheckState();
        }
        
    }

    public void UpdateState(int state)
    {
        this.state = state switch
        {
            0 => GameState.Play,
            1 => GameState.Pause,
            2 => GameState.Dead,
            3 => GameState.Destroy,
            _ => GameState.Play,
        };
        CheckState();
    }
    
    private void CheckState()
    {
        switch (state)
        {
            case GameState.Play:
                pause.SetActive(false);
                Time.timeScale = 1;
                break;
            case GameState.Pause:
                pause.SetActive(true);
                Time.timeScale = 0;
                break;
            case GameState.Destroy:
                Time.timeScale = 1;
                Destroy(gameObject);
                break;
        }
    }

    public void Respawn()
    {
        state = GameState.Play;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = checkpoint;
        player.GetComponent<PlayerHealth>().Respawn();
        
    }
}
