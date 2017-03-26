using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TimerScript : MonoBehaviour 
{
	public Text timerText;
	private int timer;

	public Text bestTimeText;

	private string bestTimeTextKey = "BestTimeText";
	private string bestTimeIntKey = "BestTimeInteger";
	private String oldBestTimeString;
	private int oldBestTimeInt;

	private TimeSpan currentTime;
	private int currentTimeInt;

	private TimeSpan baseTime;
	private String baseTimeText;
	//private TimeSpan bestTime;

	// Use this for initialization
	void Start ()
    {
        //Time.timeScale = 100f;
		baseTime = TimeSpan.Zero;
		baseTimeText = string.Format("{0:00}:{1:00}", baseTime.Minutes + (60 * baseTime.Hours), baseTime.Seconds);
		bestTimeText.text = "Time: " + PlayerPrefs.GetString(bestTimeTextKey, baseTimeText); 
    }
	
	// Update is called once per frame
	void Update ()
    {
		currentTime = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
		currentTimeInt = currentTime.Seconds + (currentTime.Minutes * 60); // convert total time to seconds
		timerText.text = string.Format("{0:00}:{1:00}", currentTime.Minutes + (60 * currentTime.Hours), currentTime.Seconds);
	}

	public void TimeScoreCheck()
	{
		oldBestTimeString = PlayerPrefs.GetString(bestTimeTextKey, baseTimeText);
		oldBestTimeInt = PlayerPrefs.GetInt(bestTimeIntKey, 0);
		if(currentTimeInt < oldBestTimeInt) // time.CompareTo(oldBestTime) returns an integer relative to if it's greater than, equal to or shorter than the compared time. -1 = shorter, 0 = same, 1 = longer
		{
			PlayerPrefs.SetInt(bestTimeIntKey, currentTimeInt); // save total seconds
			PlayerPrefs.SetString(bestTimeTextKey, timerText.text); // save the timer string
			bestTimeText.text = "Time: " + string.Format("{0:00}:{1:00}", currentTime.Minutes + (60 * currentTime.Hours), currentTime.Seconds); // set high score for time
		}

		PlayerPrefs.Save();
	}
		
}
