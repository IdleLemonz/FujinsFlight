using UnityEngine;
using InControl;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class UIManager : MonoBehaviour
{
    InputDevice device;

    public GameObject player;
    public GameObject newCamera;

    public string sceneName = "main";

    public Sprite fullHealth, twoHealth, oneHealth, noHealth, happyFace, sadFace, gameOver, winner;
    public Image Face, Hearts, title;

    private GameObject[] loadingObjects;
    private GameObject[] gameOverObjects;
    private GameObject[] pausedObjects;
    private GameObject[] pausedOnlyObjects;
    private Blur blur;
    private Health health;

	private TimerScript timer; 
   
    // Use this for initialization
    void Start ()
    {
        health = player.GetComponent<Health>();
        blur = newCamera.GetComponent<Blur>();

        Time.timeScale = 1f;
        loadingObjects = GameObject.FindGameObjectsWithTag("Load");
        pausedObjects = GameObject.FindGameObjectsWithTag("OnPause");
        gameOverObjects = GameObject.FindGameObjectsWithTag("OnGameOver");
        pausedOnlyObjects = GameObject.FindGameObjectsWithTag("PauseOnly");
        HidePaused();
        HideGameOver();
        HidePausedOnly();
        
		timer = FindObjectOfType<TimerScript>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        device = InputManager.ActiveDevice;

        if (device.MenuWasPressed || Input.GetKeyDown(KeyCode.Escape)) 
        { 
            if (Time.timeScale == 1)
            { 
                ShowPaused();
                ShowPausedOnly();
            }
            else if(Time.timeScale == 0)
            {
                HidePaused();
                HidePausedOnly();
            }
        }
        Health();
    }

    public void Reload()
    {
        blur.enabled = true;
        foreach (GameObject g in loadingObjects)
        {
            g.SetActive(true);
        }
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void ShowPaused()
    {
        foreach(GameObject g in pausedObjects)
        {
            g.SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void HidePaused()
    {
        foreach (GameObject g in pausedObjects)
        {         
            g.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void ShowPausedOnly()
    {
        foreach (GameObject g in pausedOnlyObjects)
        {
            g.SetActive(true);
        }
        Time.timeScale = 0;
    }

    public void HidePausedOnly()
    {
        foreach (GameObject g in pausedOnlyObjects)
        {
            g.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        blur.enabled = true;
        foreach (GameObject g in loadingObjects)
        {
            g.SetActive(true);
        }
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Health()
    {
        if (health.CurrentLives == 3)
        {
            Face.sprite = happyFace;
            Hearts.sprite = fullHealth;
        }
        if (health.CurrentLives == 2)
        {
            Face.sprite = happyFace;
            Hearts.sprite = twoHealth;
        }
        if (health.CurrentLives == 1)
        {
            Face.sprite = sadFace;
            Hearts.sprite = oneHealth;
        }
        if (health.CurrentLives == 0)
        {
            Face.sprite = sadFace;
            Hearts.sprite = noHealth;

            EndGame();
        }
    }

    public void ShowGameOver()
    {
        foreach (GameObject g in gameOverObjects)
        {
            g.SetActive(true);
        }
    }

    public void HideGameOver()
    {
        foreach (GameObject g in gameOverObjects)
        {
            g.SetActive(false);
        }
    }

    public void EndGame()
    {
        blur.enabled = true;
        title.sprite = gameOver;
        ShowGameOver();
        ShowPaused();
    }

    public void WinGame()
    {
        blur.enabled = true;
        title.sprite = winner;
		timer.TimeScoreCheck();
        ShowGameOver();
        ShowPaused();
    }
}