using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Object persistence between scenes based on https://learn.unity.com/tutorial/implement-data-persistence-between-scenes/
public class UpgradesManagerScript : MonoBehaviour
{
    public static UpgradesManagerScript Instance;

    [SerializeField]
    private int startingTimeLimitMinutes;
    
    [SerializeField]
    private float startingTimeLimitSeconds;

    public enum InputCommand { UpgradeTime };

    //THESE VARIABLES HOLD THE VALUES THAT THE PLAYER HAS UPGRADED THEM TO
    private int   timeLimitMinutes;
    private float timeLimitSeconds;

    //Awake is called as soon as the object is created
    private void Awake()
    {
        if (Instance != null) {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        timeLimitMinutes = startingTimeLimitMinutes;
        timeLimitSeconds = startingTimeLimitSeconds;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //scrap = HUDManager.scrapCount; // Updates scrap count for upgrade use.
    }

    public int getTimeLimitMinutes() {
        return timeLimitMinutes;
    }

    public float getTimeLimitSeconds() {
        return timeLimitSeconds;
    }

    public int getScrap() {
        return HUDManager.scrapCount;
    }

    public void upgradeTime() {
        if (HUDManager.scrapCount >= 5) {
            timeLimitSeconds += 10;
            if (timeLimitSeconds >= 60) {
                timeLimitSeconds -= 60;
                timeLimitMinutes += 1;
            } 
            HUDManager.scrapCount -= 5;
            FindObjectOfType<MainMenuAudio>().Play("UpgradeNoise");
        }
    }

    public void upgradeSpeed() {
        if (HUDManager.scrapCount >= 5 && playerMovement.moveSpeed < 10f) {
            playerMovement.moveSpeed += 0.2f;
            HUDManager.scrapCount -= 5;
            FindObjectOfType<MainMenuAudio>().Play("UpgradeNoise");
        }
    }

}
