using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI seedCountText1;
    [SerializeField] private TextMeshProUGUI seedCountText2;
    [SerializeField] private TextMeshProUGUI seedCountText3;

    [SerializeField] private TextMeshProUGUI waterCountText;

    public static int currentWaterOnWateringCan;
    public int initialSceneBuildIndex;


    public static GameManager Instance { get; private set; }


    private void Start()
    {
        StartTimer();

        GetAllSeedsOnScene();

        SceneIndexHolder.InitialSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        initialSceneBuildIndex = SceneIndexHolder.InitialSceneBuildIndex;

    }


    public int GetAllSeedsOnScene()
    {
        var foundObjects = Object.FindObjectsOfType<Seed>();
        int seeds = foundObjects.Length;

        return seeds;
    }

    public int GetAllPlantsOnScene()
    {
        var foundObjects = Object.FindObjectsOfType<ReceptorPlanta>();
        int plants = foundObjects.Length;

        return plants;

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

        seedCountText1.text = "x0";
        seedCountText2.text = "x0";
        seedCountText3.text = "x0";

    }

    //UI Seeds
    public void UpdateSeedUI(string seedType, int seedCount)
    {
        // Actualizar el texto de la UI seg�n el tipo de semilla
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

    }
    private void WinScreen()
    {
        SceneManager.LoadScene("Winner");
    }
    private void OnEnable()
    {
        HealthPlayer.sceneDead += GameOver;
        Player.sceneWinner += WinScreen;
        Player.sceneLosser += GameOver;
    }
    private void OnDisable()
    {
        HealthPlayer.sceneDead -= GameOver;
        Player.sceneWinner -= WinScreen;
        Player.sceneLosser -= GameOver;
    }


}