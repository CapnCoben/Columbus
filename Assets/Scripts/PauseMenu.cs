using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject PauseMenuUI;
    public Button button;
    public GameObject DestinationUI;
    public GameObject fastTravelUI;


    private void Start()
    {
        fastTravelUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
          
        }
        return;


    }
    public void ChooseDestinationSpain()
    {
        SceneManager.LoadScene("Spain");
        Time.timeScale = 1f;

    }

    public void ChooseDestinationNextIsland()
    {
        SceneManager.LoadScene("Island2");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            DestinationUI.SetActive(true);
            gameIsPaused = (true);
        }
    }



     void Pause()
     {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
     }

     public void Resume()
     {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
     }

    public void Map()
    {
        //gameIsPaused = true;
        Time.timeScale = 0f;
        fastTravelUI.SetActive(true);

    }


    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Game...");
        Application.Quit();
    }



}