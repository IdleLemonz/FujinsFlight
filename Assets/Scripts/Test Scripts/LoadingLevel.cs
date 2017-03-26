using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingLevel : MonoBehaviour
{
    public Image title;
    public string sceneName = "main";

    private GameObject[] loadingObjects;
    // Use this for initialization
    void Start ()
    {
        loadingObjects = GameObject.FindGameObjectsWithTag("Load");

        foreach (GameObject g in loadingObjects)
        {
            g.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void Load()
    {
        foreach (GameObject g in loadingObjects)
        {
            g.SetActive(true);
        }
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
