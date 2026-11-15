using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;



public class EndLock : MonoBehaviour
{
    
    //gameobject array
    public GameObject lockPrefab;
    public float spacing = .0010f;
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
        if (combos.Count==0)
        {
            combos = new List<int>();

            for (int i = 0; i < numInput; i++)
            {
                combos.Add(5); // placeholder values
            }
        }
        InstantiatePrefabs();
    }


    public void InstantiatePrefabs()
    {
        if (lockPrefab == null)
        return;

        Renderer parentRenderer = GetComponent<Renderer>();
        Renderer prefabRenderer = lockPrefab.GetComponent<Renderer>();

        float parentWidth = parentRenderer != null ? parentRenderer.bounds.size.x : 0f;
        float prefabWidth = prefabRenderer != null ? prefabRenderer.bounds.size.x : 1f;

        float totalRequiredWidth = (prefabWidth * numInput) + (spacing * (numInput - 1));
        float startX = 0f;

        if (parentWidth > 0f)
        {
            startX = -((totalRequiredWidth - prefabWidth) / 2f);
        }

        

        for (int i = 0; i < numInput; i++)
        {
            // Vector3 spawnPosition = transform.position + new Vector3(i * spacing, 0f, 0f);
            GameObject lockInstance = Instantiate(lockPrefab, transform);
            lockInstance.name = "LockNum_" + (i + 1);
            float xPos = startX + (i * (prefabWidth + spacing));
            lockInstance.transform.localPosition = new Vector3(xPos, 0f, .05f);

            LockCombo combo = lockInstance.GetComponent<LockCombo>();
            if (combo != null)
            {
                combo.lockItem = this;
            }
            Debug.Log($"<color=yellow>Created new {lockInstance} at {xPos}</color>");
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
        
        if (index < 0 || index >= combos.Count || index >= locks.Count)
        {
            Debug.LogWarning("Invalid index: " + index);
            return false;
        }

        bool isCorrect = lockobj.GetComponent<LockCombo>().currentNum == combos[index];

        if (isCorrect)
        {
            Debug.Log($"<color=green>{lockobj} is true!</color>");
            status[index] = true;
            audioSource.PlayOneShot(clickSFX);
        }
        else
        {
            Debug.Log($"<color=red>{lockobj} is false! {lockobj.GetComponent<LockCombo>().currentNum} does not equal {combos[index]}</color>");
            status[index] = false;
            audioSource.PlayOneShot(clickSFX);
        }

        return isCorrect;
    }


    public void checkStatus()
    {
        bool allCorrect = true;

        for (int i = 0; i < locks.Count; i++)
        {
            bool correct = CheckLock(locks[i], i);
            Debug.Log($"<color=orange>{correct}</color>");
            status[i] = correct;

            if (!correct)
            {
                allCorrect = false;
            }
        }

        if (allCorrect && locks.Count > 0)
        {
            Debug.Log($"<color=purple>{allCorrect} is true!</color>");
            gameWon = true;
        }
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
