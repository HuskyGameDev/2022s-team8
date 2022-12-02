using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class MainMenuAudio : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup sfxMixer, bgmMixer;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            if (s.isBGM)
            {
                s.source.outputAudioMixerGroup = bgmMixer;
                s.source.loop = true;
            }
            else
            {
                s.source.outputAudioMixerGroup = sfxMixer;
            }

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            DontDestroyOnLoad(s.source);

        }
        Play("MainMenuBGM");
    }

    // Start is called before the first frame update
    void Start()
    {
        sfxMixer.audioMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume", 1)) * 20);
        bgmMixer.audioMixer.SetFloat("BGM", Mathf.Log10(PlayerPrefs.GetFloat("bgmVolume", 1)) * 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            foreach (Sound s in sounds)
            {
                print(s.source.volume);
            }
        }
    }

    public void Play(string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
       s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
