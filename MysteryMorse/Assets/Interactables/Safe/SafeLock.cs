using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SafeLock : MonoBehaviour, IScrollable
{

    //the number the player is on
    public int currentNum = 0;
    //min and max values
    public int minNum = 0;
    public int maxNum = 9;
    //public float pos0x;
    //public float pos9x;
    //restrict movement speed
    //public float speedRestrict;
    //sprite array
    public Sprite[] sprites;
    


    void Start()
    {
       
    }
    public void WasScrolled(float frameScrollValue)
    {
        //Increment current num based on scroll value
        currentNum -= Mathf.RoundToInt(frameScrollValue);
        //if input was positive
        
        //clamp based on min and max
        //if lower than min num
        if (currentNum < minNum) 
        { 
            currentNum = maxNum;
            //reset sprite to position of 9
            //gameObject.transform.position = new Vector3 (pos9x,transform.position.y, transform.position.z);
        }
        //if higher than max num
        if (currentNum > maxNum) 
        { 
            currentNum = minNum;
            //reset sprite to position of 0
            //gameObject.transform.position = new Vector3 (pos0x, transform.position.y, transform.position.z);
        }
        Debug.Log("lock number" + currentNum);

        //change sprite of safe
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[currentNum];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
