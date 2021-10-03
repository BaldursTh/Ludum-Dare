using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Play, Pause, Dead
    }
    public GameState state;
    public GameObject pause;
    public static GameManager instance;

    public static Vector3 checkpoint = new Vector3();
    public Vector3 start;

    private void Awake()
    {
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
        state = GameState.Play;
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
        switch (state)
        {
            case 0:
                this.state = GameState.Play;
                break;
            case 1:
                this.state = GameState.Pause;
                break;
            case 2:
                this.state = GameState.Dead;
                break;
            default:
                this.state = GameState.Play;
                break;
        }
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
