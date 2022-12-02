using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

//Control Settings Code based on https://www.youtube.com/playlist?list=PLX-uZVK_0K_5X84_toCOlmXrYT5RGJEMO
public class SettingsManager : MonoBehaviour
{

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private GameObject currentKey;

    private float sfxGameVolume, bgmGameVolume;

    public AudioMixerGroup sfx;
    public AudioMixerGroup bgm;

    [SerializeField] public TMP_Text up, down, left, right, sfxVolume, bgmVolume;
    [SerializeField] public Slider sfxVolumeControl, bgmVolumeControl;
    

    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Up",(KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        sfxGameVolume = PlayerPrefs.GetFloat("sfxVolume", 1);
        bgmGameVolume = PlayerPrefs.GetFloat("bgmVolume", 1);


        if (SceneManager.Equals(SceneManager.GetActiveScene(), SceneManager.GetSceneByName("Options")))
        {
            up.text = keys["Up"].ToString();
            left.text = keys["Left"].ToString();
            down.text = keys["Down"].ToString();
            right.text = keys["Right"].ToString();
            sfxVolume.text = (sfxGameVolume * 100).ToString("0.0") + "%";
            sfxVolumeControl.SetValueWithoutNotify(sfxGameVolume);
            bgmVolume.text = (bgmGameVolume * 100).ToString("0.0") + "%";
            bgmVolumeControl.SetValueWithoutNotify(bgmGameVolume);
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<TMP_Text>().text = e.keyCode.ToString(); 
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        FindObjectOfType<MainMenuAudio>().Play("MenuClick");
        currentKey = clicked;
    }

    public void SFXVolumeSlider(float vol)
    {
        sfxGameVolume = vol;
        sfxVolume.text = (vol * 100).ToString("0.0") + "%";
    }

    public void BGMVolumeSlider(float vol)
    {
        bgmGameVolume = vol;
        bgmVolume.text = (vol * 100).ToString("0.0") + "%";
    }

    public void SaveSettings()
    {
        foreach(var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.SetFloat("sfxVolume", sfxGameVolume);
        PlayerPrefs.SetFloat("bgmVolume", bgmGameVolume);

        PlayerPrefs.Save();

        sfx.audioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume", 1)) * 20);
        bgm.audioMixer.SetFloat("BGM", Mathf.Log10(PlayerPrefs.GetFloat("bgmVolume", 1)) * 20);
        FindObjectOfType<MainMenuAudio>().Play("MenuClick");
    }

    public KeyCode getKey(string actionName)
    {
        return keys[actionName];
    }

    public float getSFXVolume()
    {
        return PlayerPrefs.GetFloat("sfxVolume", 1);
    }

    public float getBGMVolume()
    {
        return PlayerPrefs.GetFloat("bgmVolume", 1);
    }
}
