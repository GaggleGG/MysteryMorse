using UnityEngine;

public class Safe : MonoBehaviour, IScrollable
{
    //the number the player is on
    private int currentNum = 0;
    //sprite array
    public Sprite[] sprites;
    //min and max values
    public int minNum = 0;
    public int maxNum = 9;

    public void WasScrolled(float frameScrollValue)
    {
        //Increment current num based on scroll value
        currentNum += Mathf.RoundToInt(frameScrollValue);
        //clamp based on min and max
        if (currentNum < minNum) currentNum = maxNum;
        if (currentNum > maxNum) currentNum = minNum;
        Debug.Log("Safe number" + currentNum);

        //change sprite of safe
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
