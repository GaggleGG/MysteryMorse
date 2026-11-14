using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sfx;
    public bool looping;
    public float delayTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(PlayAudio());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayAudio()
    {
        
        audioSource.PlayOneShot(sfx);
        yield return new WaitForSeconds(sfx.length + delayTime);
        if(looping) StartCoroutine(PlayAudio());
        
    }
}
