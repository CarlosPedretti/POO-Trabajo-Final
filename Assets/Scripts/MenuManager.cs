using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    int initialSceneBuildIndex;

    private void Start()
    {
        initialSceneBuildIndex = SceneIndexHolder.InitialSceneBuildIndex;
    }

    public void Play()
    {

        SceneManager.LoadScene(initialSceneBuildIndex);
    }

    public void InitialPlay()
    {

        SceneManager.LoadScene("Game");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        Debug.Log("Exit...");
        Application.Quit();
    }



    public void RestartMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        int nextSceneBuildIndex = initialSceneBuildIndex + 1;
        SceneManager.LoadScene(nextSceneBuildIndex);
    }
}