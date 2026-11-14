using Unity.VisualScripting;
using UnityEngine;
 
public class Lamp : ScrollToPoint
{
    [SerializeField] Light LampLight;
    [SerializeField] Transform CordTrans;
    Vector3 cordStartPos;
    [SerializeField] Vector3 cordEndPos;

    float prevSlideVal;

    public enum GameState
    {
        On,
        Off
    }


    //Amount pulled to flip lamp
    float flipThreshhold = 70;



    bool onState = true;
    //float cordPosition;
    bool isScrolling;
    bool flippable;


    //An enumerator to be sent to different events for event specific actions.
    GameState LampState;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
   

    //Activates once the mouse is scrolling over the object
    public void WasScrolled(float frameScrollValue)
    {

        if(frameScrollValue > 0)
        {
            
        }





        /*
        //Adds the scrolling value to the cordPosition to create a value.
        cordPosition += frameScrollValue;
        cordPosition = Mathf.Clamp(cordPosition, minCord, maxCord);
        if (frameScrollValue == 0)
        {
            isScrolling = false;
            flippable = true;
        }
        else
        {
            isScrolling = true;
        }
            Debug.Log(cordPosition);
        if (flippable == true)
        {
         if ((cordPosition == maxCord) && (onState == false))
                {
                     onState = true;
                    cordPosition = 0;
                    LampState = GameState.On;
                    flippable = false;
                }
         else if((cordPosition == maxCord) && (onState == true))
         {
                    onState = false;
                    cordPosition = 0;
                    LampState = GameState.Off;
                    flippable = false;

         }
        }
        Debug.Log($"Frame Scroll value: {frameScrollValue}");
       

       
        
        LightCheck();

        */
    }


    //flips to opposite light state
    public void FlipLight()
    {
        onState = !onState;
        SetLight(onState);
    }

    //sets to specific light on state (true = on, false = off)
    public void SetLight(bool isOn)
    {
        onState = isOn;
        LampLight.gameObject.SetActive(onState);
    }




    // Update is called once per frame
    void Update()
    {
        if(!isScrolling)
        {
            slideValue = Mathf.MoveTowards(slideValue, 0, 1 * Time.deltaTime);
        }
    }
}
