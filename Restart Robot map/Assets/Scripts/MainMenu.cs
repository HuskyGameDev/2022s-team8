using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public string scene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // To be used with the main menu buttons. Used to change what menu you are on/start the game.
    public void buttonChangeScene(string scene) {
        FindObjectOfType<MainMenuAudio>().Play("MenuClick");
        SceneManager.LoadScene(scene); // Loads the next menu or main game.
    }


    public void Quit() {
        FindObjectOfType<AudioManager>().Play("MenuClick");
        Application.Quit(); // Simply closes the running application
    }
}
