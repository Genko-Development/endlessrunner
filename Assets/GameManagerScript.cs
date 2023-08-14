using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    private bool IsPaused = false;
    private bool IsAlive = true;

    //script
    private PlayerMovementScript PlayerScript;


    
    public AudioSource BackgroundMusic;
    public AudioSource HitSound;
    public AudioSource ButtonSound;

    public Slider VolumeSlider;
    //float m_MySliderValue;

    public float PlatformSpeed = 5f;
    private int nextUpdate = 1;

    //Texts
    public TextMeshProUGUI Score;
    private int ScoreInt;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI HighScore;

    public TextMeshProUGUI PauseScore;
    

    //Canvases
    public GameObject GameOverlayCanvas;
    public GameObject DeathScreen;
    public GameObject PauseScreen;
    public GameObject OptionsScreen;

    public void Options()
    {
        ButtonSound.Play();
        OptionsScreen.SetActive(true);
        PauseScreen.SetActive(false);
    }
    public void Pause()
    {
        ButtonSound.Play();

        PauseScore.text = ScoreInt.ToString();
        Time.timeScale = 0;
        GameOverlayCanvas.SetActive(false);
        PauseScreen.SetActive(true);
        IsPaused = true;
    }

    public void Resume()
    {
        ButtonSound.Play();

        Time.timeScale = 1;
        GameOverlayCanvas.SetActive(true);
        PauseScreen.SetActive(false);
        IsPaused = false;
    }


    public void RestartButton()
    {
        ButtonSound.Play();

        PlatformSpeed = 5f;
        Time.timeScale = 1;
        PlayerScript.IsHit = false;
        GameOverlayCanvas.SetActive(true);
        DeathScreen.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Mute()
    {
        PlayerPrefs.SetFloat("Volume", 0f);
        BackgroundMusic.volume = 0f;
    }

    public void Volume()
    {
        PlayerPrefs.GetFloat("Volume");
        BackgroundMusic.volume = VolumeSlider.value;
    }

    public void Save()
    {
        ButtonSound.Play();

        //set playerprefs Volume to volumeslider.value
        PlayerPrefs.SetFloat("Volume", VolumeSlider.value);
        OptionsScreen.SetActive(false);
        PauseScreen.SetActive(true);
    }

    void Start()
    {
        HitSound.volume = 0f;

        VolumeSlider.value = PlayerPrefs.GetFloat("Volume");
        PlayerScript = GameObject.Find("Player").GetComponent<PlayerMovementScript>();
        GameOverlayCanvas = GameObject.Find("Overlay");

        Score.text = "0";

        //set audio volume
        BackgroundMusic.volume = PlayerPrefs.GetFloat("Volume");
        
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (IsPaused)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Resume();
            }
        }

        if (IsAlive)
        {
            if (PlayerScript.IsHit == true)
            {
                IsAlive = false;
                PlayerHit();
            }
        }

        if (PlayerScript.IsHit == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestartButton();
            }
        }

        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            if (ScoreInt % 10 == 0)
            {
                PlatformSpeed += 0.5f;
            }
            UpdateEverySecond();
        }

    }

    void UpdateEverySecond()
    {
        ScoreInt++;
        Score.text = ScoreInt.ToString();
    }

    void PlayerHit()
    {
        BackgroundMusic.volume = 0f;
        HitSound.volume = 1f;
        HitSound.Play();
        

        PlatformSpeed = 0;
        Time.timeScale = 0;
        GameOverlayCanvas.SetActive(false);
        //show the death screen
        DeathScreen.SetActive(true);
        finalScore.text = ScoreInt.ToString();

        if (PlayerPrefs.GetInt("HighScore") < ScoreInt)
        {
            PlayerPrefs.SetInt("HighScore", ScoreInt);
            finalScore.color = Color.green;
            Debug.Log("color test");
        }
        else
        {
            finalScore.color = Color.grey;
        }

        HighScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();

    }
}
