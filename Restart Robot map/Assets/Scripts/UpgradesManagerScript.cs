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
        //DEBUG CODE
        if(Input.GetKeyDown(KeyCode.L))
        {
            timeLimitMinutes += 1;
        }
    }

    public int getTimeLimitMinutes() {
        return timeLimitMinutes;
    }

    public float getTimeLimitSeconds() {
        return timeLimitSeconds;
    }
}
