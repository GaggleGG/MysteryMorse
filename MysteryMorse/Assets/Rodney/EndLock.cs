using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;



public class EndLock : MonoBehaviour
{
    
    //gameobject array
    public GameObject lockPrefab;
    public float spacing = 2.0f;
    //combinations


    public int numInput = 5;
    public List<int> combos;
    //audio source and clip
    public AudioSource audioSource;
    public AudioClip clickSFX;



    //lock status, when all values are true trigger the end of the game
    bool[] status;
    List<GameObject> locks = new List<GameObject>();

    public bool gameWon = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        status = new bool[numInput];
        InstantiatePrefabs();
    }


    public void InstantiatePrefabs()
    {
        for (int i = 0; i < numInput; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(i * spacing, 0f, 0f);
            GameObject lockInstance = Instantiate(lockPrefab, spawnPosition, Quaternion.identity, transform);
            lockInstance.name = "LockNum_" + (i + 1);
            locks.Add(lockInstance);
        }
    }

    public bool CheckLock(GameObject lockobj, int index)
    {
        // if (lockobj.GetComponent<LockCombo>().currentNum == combos[index])
        // {
        //     Debug.Log("<color=green>Correct Lock</color>");
        //     bool isUnlocked = true;
        //     if (isUnlocked)
        //     {
        //         status[index] = lockobj.GetComponent<LockCombo>().currentNum == combos[index];
        //         return true;
        //     }
        //     return true;
        // }
        // else
        // {
        //     return false;  
        // }
        bool isCorrect = lockobj.GetComponent<LockCombo>().currentNum == combos[index];

        if (isCorrect)
        {
            status[index] = true;
            audioSource.PlayOneShot(clickSFX);
        }
        else
        {
            status[index] = false;
            audioSource.PlayOneShot(clickSFX);
        }

        return isCorrect;
    }


    


    // Update is called once per frame
    void Update()
    {
        // if (locks[0].GetComponent<LockCombo>().currentNum == combos[0])
        // {
        //     Debug.Log("Lock1 correct");
        //     lock1 = true;
        //     if (lock1SFX)
        //     {
        //         audioSource.PlayOneShot(clickSFX);
        //         lock1SFX = false;
        //     }
        // }
        // else
        // {
        //     lock1 = false;
        //     lock1SFX = true;
        // }
        
        // if (locks[1].GetComponent<LockCombo>().currentNum == combos[1])
        // {

        //     Debug.Log("Lock2 correct");
        //     lock2 = true;
        //     if (lock2SFX)
        //     {
        //         audioSource.PlayOneShot(clickSFX);
        //         lock2SFX = false;
        //     }
        // }
        // else
        // {
        //     lock2 = false;
        //     lock2SFX = true;
        // }
        // if (locks[2].GetComponent<LockCombo>().currentNum == combos[2])
        // {
        //     lock3 = true;
        //     Debug.Log("Lock3 correct");
        //     if (lock3SFX)
        //     {
        //         audioSource.PlayOneShot(clickSFX);
        //         lock3SFX = false;
        //     }
        // }
        // else
        // {
        //     lock3 = false;
        //     lock3SFX = true;
        // }
        // if (locks[3].GetComponent<LockCombo>().currentNum == combos[3])
        // {
        //     lock4 = true;
        //     Debug.Log("Lock4 correct");
        //     if (lock4SFX)
        //     {
        //         audioSource.PlayOneShot(clickSFX);
        //         lock4SFX = false;
        //     }
        // }
        // else
        // {
        //     lock4 = false;
        //     lock4SFX = true;
        // }
        // if (lock1 && lock2 && lock3 && lock4)
        // {
        //     Debug.Log("all locks correct");
        // }
    
        bool allCorrect = true;

        for (int i = 0; i < locks.Count; i++)
        {
            bool correct = CheckLock(locks[i], i);
            status[i] = correct;

            if (!correct)
            {
                allCorrect = false;
            }
        }

        if (allCorrect && locks.Count > 0)
        {
            Debug.Log("All locks correct");
            gameWon = true;
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
