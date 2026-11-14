using UnityEngine;

public class Lamp : MonoBehaviour, IScrollable 
{
    //Creates value to track the scroll movement and on state
    float minCord = 0;
    float maxCord = 10;
    bool onState = true;
    float cordPosition;
    //Activates once the mouse is scrolling over the object
    public void WasScrolled(float frameScrollValue)
    {
        //Adds the scrolling value to the cordPosition to create a value.
        cordPosition += frameScrollValue;
        cordPosition = Mathf.Clamp(cordPosition, minCord, maxCord);

        Debug.Log(cordPosition);
        if ((cordPosition == maxCord) && (onState == false))
        {
             onState = true;
            cordPosition = 0;
        }
        if ((cordPosition == maxCord) && (onState == true))
        {
            onState = false;
            cordPosition = 0;
        }
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
