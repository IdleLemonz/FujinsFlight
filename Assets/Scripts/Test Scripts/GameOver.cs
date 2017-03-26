using UnityEngine;
using InControl;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GameOver : MonoBehaviour
{
    private GameObject[] gameOverObjects;
    private Blur blur;

    public GameObject newCamera;

	// Use this for initialization
	void Start ()
    {
        blur = newCamera.GetComponent<Blur>();
    }
	
	// Update is called once per frame
	void Update ()
    {
      
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        blur.enabled = !blur.enabled;
    }
}
