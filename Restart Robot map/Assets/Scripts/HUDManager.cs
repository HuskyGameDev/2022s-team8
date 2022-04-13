using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//THIS SCRIPT IS BASED ON A TUTORIAL MADE BY I_Am_Err00r ON YOUTUBE. Credit to him for setting up a lot of this logic.
//LINK TO HIS CHANNEL: https://www.youtube.com/channel/UCkeuMRVpRBFJ-nT12AD4GaA

public class HUDManager : MonoBehaviour
{
    //This is a reference to the Text component on the HUD gameobject that will display how much time has passed since the level started
    [SerializeField]
    private Text timeText = null;
	
    //Parameter: How many minutes the player has until time runs out at level start
    [SerializeField]
	private int timeLimitMinutes;
	
    //Parameter: How many seconds the player has until time runs out at level start
    [SerializeField]
	private float timeLimitSeconds;
	
	private bool timeIsUp = false;

    private void Start()
    {
		timeLimitSeconds = timeLimitSeconds + 0.99f;
    }

    // Update is called once per frame
    private void Update()
    {
        //A method that constantly adds to the timeInLevel value as long as player is alive
        AddTime();

    }

    private void AddTime()
    {
		//timeText.text is the exact text string that is displayed in the timer
		
		if (timeIsUp == false) {//continue to count down timer
			timeLimitSeconds -= Time.deltaTime;
			if (timeLimitSeconds < 0) {
				if (timeLimitMinutes > 0) {
					timeLimitSeconds = 59.99f;
					timeLimitMinutes = timeLimitMinutes - 1;
				} else {
					timeLimitSeconds = 0.0f;
					timeLimitMinutes = 0;
					timeIsUp = true;
				}
			}
	        timeText.text = timeLimitMinutes.ToString("D2") + ":" + ((int)Math.Floor(timeLimitSeconds)).ToString("D2");
		} else {//flash timer because time is up
		//timeLimitSeconds: time left in current state
		//timeLimitMinutes: current flash state, 1 = timer is visible, 0 = timer is invisible
			timeLimitSeconds -= Time.deltaTime;
			if (timeLimitSeconds < 0) {
				timeLimitSeconds = 0.5f;//time spent in each state
				timeLimitMinutes = timeLimitMinutes - 1;
				if (timeLimitMinutes < 0) {
					timeLimitMinutes = 1;
				}
			}
			if (timeLimitMinutes == 0) {
				timeText.text = "00:00";
			} else {
				timeText.text = "";
			}
		}

    }

}