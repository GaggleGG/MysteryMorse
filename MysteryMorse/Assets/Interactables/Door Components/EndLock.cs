using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;



public class EndLock : MonoBehaviour
{
    
    //gameobject array
    public GameObject lockPrefab;
    public float spacing = .0010f;
    float minY = -0.05f;
    float maxY = 0.05f;


    public float scale = .01f;
    float totalSpacing;
    float startY;


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

        totalSpacing = (numInput - 1) * spacing;

        startY = minY;
        

        for (int i = 0; i < numInput; i++)
        {
           float yPos = startY + (i * spacing);
            Vector3 spawnPos = transform.position + new Vector3(.01f, yPos, 0f);

            // Vector3 spawnPosition = transform.position + new Vector3(i * spacing, 0f, 0f);
            // GameObject lockInstance = Instantiate(lockPrefab, spawnPos, Quaternion.Euler(90f, -90f, -90f), transform);
            // lockInstance.transform.localScale = new Vector3(scale,scale,scale);
            // lockInstance.transform.localRotation = Quaternion.Euler(0f, -90f, -90f);

            GameObject lockInstance = Instantiate(lockPrefab, transform);
            lockInstance.transform.localPosition = new Vector3(0f, yPos, 0f);
            lockInstance.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
            lockInstance.transform.localScale = Vector3.one * 0.01f;

            
            
            lockInstance.name = "LockNum_" + (i + 1);
            
            LockCombo combo = lockInstance.GetComponent<LockCombo>();
            if (combo != null)
            {
                combo.lockItem = this;
            }
            Debug.Log($"<color=yellow>Created new {lockInstance} at {yPos}</color>");
            locks.Add(lockInstance);
        }
    }

    public bool CheckLock(GameObject lockobj, int index)
    {
        
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



    
    private void SoundCheck(bool lockCheck)
    {
        if (lockCheck)
        {
            audioSource.PlayOneShot(clickSFX);
            lockCheck = false;
        }
    }
}
