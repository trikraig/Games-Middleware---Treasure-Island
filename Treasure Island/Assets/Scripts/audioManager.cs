using System;
using UnityEngine;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{
    //Array to hold all audio to be controlled in manager and is emitted by the main.
    public Sound[] sounds;

    //Singular instance to allow easy location.
    public static audioManager instance;

    // Use this for initialization
    void Awake()
    {
        //Checks and sets the singular instance to this if does not exist.
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        //Loops through every sound stored int the array and creates a source component. Applies the settings to the component, more can be added.
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    //Background music state machine. Only one can be played at a time. Could store previous track as a reference however is a short demo.
    public void ChangeMusic(int trackNumber)
    {
        switch (trackNumber)
        {
            case 1:
                Play("Theme1"); //Use on pirate ship.
                break;

            case 2:
                Stop("Theme1");
                Play("Theme2"); //Use on island.
                break;
            case 3:
                Stop("Theme2");
                Play("Theme3"); //Use as fight boss.
                break;
            case 4:
                Stop("Theme3");
                Play("TombBG"); //Use as enter tomb.
                break;


        }
    }

    //Searches array to find passed in string of desired audio clip. If null catches and sends warning to log.
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " is not found.");
            return;
        }
        s.source.Play();
    }

    //Same as play function only stops the desired audio clip.
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " is not found.");
            return;
        }
        s.source.Stop();
    }
}
