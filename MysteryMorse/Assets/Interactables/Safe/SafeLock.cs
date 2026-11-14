using UnityEngine;

public class SafeLock : MonoBehaviour, IScrollable
{

    //the number the player is on
    public int currentNum = 0;
    //min and max values
    public int minNum = 0;
    public int maxNum = 9;
    public float pos0x;
    public float pos9x;
    //restrict movement speed
    public float speedRestrict;

    public void WasScrolled(float frameScrollValue)
    {
        //Increment current num based on scroll value
        currentNum -= Mathf.RoundToInt(frameScrollValue);
        //clamp based on min and max
        if (currentNum < minNum) 
        { 
            currentNum = maxNum;
            //reset sprite
            gameObject.transform.position = new Vector3 (pos9x,transform.position.y, transform.position.z);
        }
        if (currentNum > maxNum) 
        { 
            currentNum = minNum;
            //reset sprite
            gameObject.transform.position = new Vector3 (pos0x, transform.position.y, transform.position.z);
        }
        Debug.Log("lock number" + currentNum);

        //change sprite of safe
        gameObject.transform.Translate((Vector3.right * frameScrollValue)/speedRestrict);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
