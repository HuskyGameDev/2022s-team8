using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtons : MonoBehaviour
{
    public enum InputCommand { UpgradeTime };
    public static InputCommand command;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void upgradeTimeButton() {
        UpgradesManagerScript.Instance.upgradeTime();
    }

    public void upgradeSpeedButton() {
        UpgradesManagerScript.Instance.upgradeSpeed();
    }

}
