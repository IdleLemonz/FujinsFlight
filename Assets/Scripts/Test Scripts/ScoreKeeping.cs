using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeping : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    private OrbInfo oI;
    private int score;
    private int oldHS;

    private GameObject[] scoreCollectables;

    // Use this for initialization
    void Start ()
    {
        highScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore", 120);
        scoreCollectables = GameObject.FindGameObjectsWithTag("ScoreCollectable");
        foreach (GameObject g in scoreCollectables)
        {
            g.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    public void AddScore(Collider col)
    {
        oI = col.gameObject.GetComponent<OrbInfo>();
        score += oI.orbValue;
        scoreText.text = "Score: " + score;

        int oldHS = PlayerPrefs.GetInt("HighScore", 120);
        if(score > oldHS)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "Best: " + score;
        }
        PlayerPrefs.Save();
    }
}
