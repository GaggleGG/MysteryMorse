using UnityEngine;

public class Safe : MonoBehaviour, IScrollable
{
    //what the safe's current number is
    private int currentNum;
    //the max number before looping
    public int maxNum = 60;
    //to control how fast safe rotates
    public float rotateDivider = 360;
    
    public void WasScrolled(float frameScrollValue)
    {
        //add scroll value and cast to int (rounding up)
        currentNum += Mathf.RoundToInt(frameScrollValue);
        //clamps to make sure that number will not be over max number or below 0
        if (currentNum > maxNum) currentNum = 0;
        if (currentNum < 0) currentNum = maxNum;

        Debug.Log("Safe number" + currentNum);

        //rotate
        gameObject.transform.Rotate((Vector3.forward * frameScrollValue) * rotateDivider,Space.World);
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
