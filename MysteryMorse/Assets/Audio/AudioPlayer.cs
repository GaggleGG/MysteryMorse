using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AudioPlayer : MonoBehaviour
{
    public Sound[] sounds;



    void Start()
    {
         foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //plays sound of name
    public void Play (string name)
    {
        //Debug.Log("playing sound: " + name);
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null && s.source != null)
        {
            s.source.Play();
        }
        else if (s == null)
        {
            Debug.LogWarning($"{s} does not contain a source sound to play");
        }
        else
        {
            Debug.LogError("could not find sound name: " + name);
        }
        
    }


    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);        

        if(s != null && s.source != null)
        {
            s.source.Stop();

            //Debug.Log("stopping sound: " + s.name);
            //Debug.Log("s.source = " + s.source.name);
        }
        else if (s.source == null)
        {
            Debug.LogWarning($"{s} does not contain a source sound to play");
        }
        else
        {
            Debug.LogError("ERROR sound " + name + " not found!");
        }

    }

    public void StopAll()
    {
        foreach(Sound s in sounds)
        {
            Stop(s.name);
        }

    }

    


}


[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch = 1;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public void Play ()
    {
        source.Play();   
    }
}
