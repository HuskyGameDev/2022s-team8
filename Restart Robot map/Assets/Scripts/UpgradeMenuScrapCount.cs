using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeMenuScrapCount : MonoBehaviour
{
    [SerializeField]
    private Text scrapCountText = null;

    private void Start()
    {
        UpdateScrapText();
    }

    private void Update()
    {
        UpdateScrapText();
    }

    private void UpdateScrapText()
    {
        scrapCountText.text = HUDManager.scrapCount.ToString();
    }
}
