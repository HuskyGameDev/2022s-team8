using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private GameObject GUITimerObject;
    private float timeRemaining;
    public int DurationSeconds;
    public GameObject timerSpriteSheet_0;//prefab to spawn
    private GameObject num1;//instance of the spawned prefab
    ////private Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = DurationSeconds;
        GUITimerObject = GameObject.Find("TimerBG");
        num1 = Instantiate(timerSpriteSheet_0, new Vector2(0, 0), Quaternion.identity);
        ////sprites = Resources.LoadAll<Sprite>("timerSpriteSheet");
    }

    // Update is called once per frame
    void Update()
    {
        int previousTime = (int)Mathf.Floor(timeRemaining);
        timeRemaining -= 1*Time.deltaTime;
        int secondsRemaining = (int)Mathf.Floor(timeRemaining % (float)60);
        int minutesRemaining = (int)Mathf.Floor(timeRemaining - secondsRemaining)/60;

        if (Mathf.Floor(timeRemaining) != previousTime) {
            if (timeRemaining <= 0 && timeRemaining > -5) {
                Debug.Log("TIME IS UP!!!");
            } else if (timeRemaining > 0) {
                Debug.Log(minutesRemaining + " minutes, " + secondsRemaining + "seconds remain");
            }
        }

        //print(sprites);
        //print("index = " + (secondsRemaining % 10));
        ////num1.GetComponent<SpriteRenderer>().sprite = sprites[1];
        
    }
}
