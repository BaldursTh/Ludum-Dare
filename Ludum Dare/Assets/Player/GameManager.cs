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



    private void Start()
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
                pause.SetActive(true);
                Time.timeScale = 0;
            }
            else if (state == GameState.Pause)
            {
                state = GameState.Play;
                pause.SetActive(false);
                Time.timeScale = 1;
            }
               
        }
        
    }
    


}
