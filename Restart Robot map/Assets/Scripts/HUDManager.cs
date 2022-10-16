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
    private Text scrapCountText = null;

    //This is a reference to the Text component on the HUD gameobject that will display how much time has passed since the level started
    [SerializeField]
    private Text timeText = null;
	
	//This is a reference to the Slider component on the HUD gameobject that will display how much health the player has
    [SerializeField]
	private Slider healthSlider = null;

	//Text component on healthbar
	[SerializeField]
	private Text healthBarText = null;

    //Parameter: How many minutes the player has until time runs out at level start
    [SerializeField]
	private int timeLimitMinutes;
	
    //Parameter: How many seconds the player has until time runs out at level start
    [SerializeField]
	private float timeLimitSeconds;
	
	private bool timeIsUp = false;
	private int scrapCount = 0;
	private bool playerIsDead;

	private int healthSliderDestination;
	private float healthSliderValue;

	[SerializeField]
	private GameObject playerObject = null;
	[HideInInspector]
	private playerMovement playerMovementScript;

//C sharp format specifiers in ToString(): https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings

    private void Start()
    {
		timeLimitSeconds = timeLimitSeconds + 0.99f;
		scrapCountText.text = "0";
		healthBarText.text = "100";
		healthSlider.value = 1;
		healthSliderValue = 1;
		healthSliderDestination = 100;


		//subscribe to the UpdateHealthEvent event
		playerMovement.UpdateHealthEvent += ChangeHealth;

		//for running method to damage player
		playerMovementScript = playerObject.GetComponent<playerMovement>();

		playerIsDead = false;
    }

    // Update is called once per frame
    private void Update()
    {
		
        AddTime();

		UpdateHealthDisplay();
		
    }

	//called by ScrapCollider.cs
	public void addScrap(int amount) {
		if (playerIsDead)
			return;
		
		scrapCount = scrapCount + amount;
		scrapCountText.text = scrapCount.ToString();
	}

    //A method that constantly adds to the timeInLevel value as long as player is alive
    private void AddTime()
    {
		if (playerIsDead && !timeIsUp)
			return;
		
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
					//KILL THE PLAYER
					playerMovementScript.DamagePlayer(999);
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

	private void UpdateHealthDisplay() {
		if (playerIsDead) {
			healthSliderDestination = 0;
		}

		//health slider value (position of the slider)
		if (healthSliderValue > playerMovementScript.maxHealth) {
			healthSliderValue = playerMovementScript.maxHealth;
		} else if (healthSliderValue < 0) {
			healthSliderValue = 0;
		} else if (Math.Abs(healthSliderDestination-healthSliderValue) <= 1) {
			healthSliderValue = healthSliderDestination;
		} else if (healthSliderValue < healthSliderDestination) {
			healthSliderValue += Time.deltaTime*(healthSliderDestination-healthSliderValue)*15;
		} else if (healthSliderValue > healthSliderDestination) {
			healthSliderValue -= Time.deltaTime*(healthSliderValue-healthSliderDestination)*15;
		}

		healthSlider.value = (int)Math.Round(healthSliderValue);
		
		//health bar text
		healthBarText.text = healthSliderDestination.ToString("F0");
		RectTransform myRectTransform = healthBarText.GetComponent<RectTransform>();
		if (healthSliderValue >= 10) {
			myRectTransform.anchoredPosition = new Vector2(-83, 0);
			healthBarText.color = Color.white;
		} else {
			myRectTransform.anchoredPosition = new Vector2(-71, 0);	
			healthBarText.color = new Color(255, 0, 0, 255);
		}

	}

	private void ChangeHealth(int amount)
	{
		if (playerIsDead)
			return;

		if (amount <= 0) {
			healthSliderDestination = 0;
			playerIsDead = true;
		} else { 
			healthSliderDestination = amount;
		}
	}

}