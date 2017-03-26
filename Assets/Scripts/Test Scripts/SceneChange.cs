using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string destination;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void LoadLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(destination);
    }
}
