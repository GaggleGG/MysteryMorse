using UnityEngine;

public class Safe : MonoBehaviour
{
    
    //gameobject array
    public GameObject[] locks;
    //combinations
    public int[] combos;
    //audio source and clip
    public AudioSource audioSource;
    public AudioClip clickSFX;
    //lock bools
    bool lock1 = false;
    bool lock2 = false;
    bool lock3 = false;
    bool lock4 = false;
    //SFX bools
    bool lock1SFX = true;
    bool lock2SFX = true;
    bool lock3SFX = true;
    bool lock4SFX = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (locks[0].GetComponent<SafeLock>().currentNum == combos[0])
        {
            Debug.Log("Lock1 correct");
            lock1 = true;
            if (lock1SFX)
            {
                audioSource.PlayOneShot(clickSFX);
                lock1SFX = false;
            }
        }
        else
        {
            lock1 = false;
            lock1SFX = true;
        }
        
        if (locks[1].GetComponent<SafeLock>().currentNum == combos[1])
        {

            Debug.Log("Lock2 correct");
            lock2 = true;
            if (lock2SFX)
            {
                audioSource.PlayOneShot(clickSFX);
                lock2SFX = false;
            }
        }
        else
        {
            lock2 = false;
            lock2SFX = true;
        }
        if (locks[2].GetComponent<SafeLock>().currentNum == combos[2])
        {
            lock3 = true;
            Debug.Log("Lock3 correct");
            if (lock3SFX)
            {
                audioSource.PlayOneShot(clickSFX);
                lock3SFX = false;
            }
        }
        else
        {
            lock3 = false;
            lock3SFX = true;
        }
        if (locks[3].GetComponent<SafeLock>().currentNum == combos[3])
        {
            lock4 = true;
            Debug.Log("Lock4 correct");
            if (lock4SFX)
            {
                audioSource.PlayOneShot(clickSFX);
                lock4SFX = false;
            }
        }
        else
        {
            lock4 = false;
            lock4SFX = true;
        }
        if (lock1 && lock2 && lock3 && lock4)
        {
            Debug.Log("all locks correct");
        }
    }

    
    private void SoundCheck(bool lockCheck)
    {
        if (lockCheck)
        {
            audioSource.PlayOneShot(clickSFX);
            lockCheck = false;
        }
    }
}
