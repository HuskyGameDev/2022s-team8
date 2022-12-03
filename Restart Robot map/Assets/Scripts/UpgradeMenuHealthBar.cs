using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeMenuHealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider = null;

    //Text component on healthbar
	[SerializeField]
	private Text healthBarText = null;

	private float healthSliderValue;

    private void Start()
    {
        UpdateHealthDisplay();
    }

    private void Update()
    {
        UpdateHealthDisplay();
    }

    private void UpdateHealthDisplay() {
        healthSliderValue = UpgradesManagerScript.Instance.getMaxHealth();

        healthSlider.value = (int)Math.Round(healthSliderValue);
        
        RectTransform myRectTransform = healthBarText.GetComponent<RectTransform>();
		myRectTransform.anchoredPosition = new Vector2(-83, 0);
		healthBarText.color = Color.white;
		healthBarText.text = healthSliderValue.ToString("F0");
	}
}
