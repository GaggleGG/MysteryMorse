using UnityEngine;

public class Safe : MonoBehaviour
{
    
    //gameobject array
    public GameObject[] locks;
    //combinations
    public int[] combos;

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
            if (locks[1].GetComponent<SafeLock>().currentNum == combos[1])
            {
                Debug.Log("Lock2 correct");
                if (locks[2].GetComponent<SafeLock>().currentNum == combos[2])
                {
                    Debug.Log("Lock3 correct");
                    if (locks[3].GetComponent<SafeLock>().currentNum == combos[3])
                    {
                        Debug.Log("Lock4 correct");
                        Debug.Log("All locks correct");
                    }
                }
            }
        }
    }
}
