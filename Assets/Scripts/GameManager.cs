using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private GameObject GameOverScreen;
    //public float delayBeforeGameOver = 1f;
    //[SerializeField] private GameObject WinScene;

    [SerializeField] private TextMeshProUGUI seedCountText1;
    [SerializeField] private TextMeshProUGUI seedCountText2;
    [SerializeField] private TextMeshProUGUI seedCountText3;

    [SerializeField] private TextMeshProUGUI waterCountText;

    public static int currentWaterOnWateringCan;


    public static GameManager Instance { get; private set; }



    private void Start()
    {
        StartTimer();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        seedCountText1.text = "";
        seedCountText2.text = "";
        seedCountText3.text = "";

    }

    //UI Seeds

    public void UpdateSeedUI(string seedType, int seedCount)
    {
        // Actualizar el texto de la UI según el tipo de semilla
        if (seedType == "Zanahoria")
        {
            seedCountText1.text = "x" + seedCount.ToString();
        }
        else if (seedType == "Calabaza")
        {
            seedCountText2.text = "x" + seedCount.ToString();
        }
        else if (seedType == "Berenjena")
        {
            seedCountText3.text = "x" + seedCount.ToString();
        }
    }


    //UI Water
    public void UpdateWaterUI()
    {
        waterCountText.text = currentWaterOnWateringCan.ToString() + "L";
    }


    //Timer.

    [SerializeField] public Image timerBar;

    public float duration = 60f;
    private float elapsedTime;
    private bool isTimerRunning;


    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= duration)
            {
                elapsedTime = duration;
                isTimerRunning = false;

                GameOver();
            }

            float remainingTime = duration - elapsedTime;
            UpdateTimerBar(remainingTime / duration);

        }
    }


    private void UpdateTimerBar(float fillAmount)
    {
        timerBar.fillAmount = fillAmount;
    }


    private void StartTimer()
    {
        isTimerRunning = true;
    }

    private void GameOver()
    {
         SceneManager.LoadScene("Dead");
         Time.timeScale = 0f;
       
    }

    /*private void ShowGameOver()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void WinScreen()
    {
        WinScene.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    */

}