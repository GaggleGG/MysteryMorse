using UnityEngine;
 
public class Lamp : MonoBehaviour, IScrollable 
{
    [SerializeField] Light LampLight;
    public enum GameState
    {
        On,
        Off
    }
    //Creates value to track the scroll movement and on state
    float minCord = 0;
    float maxCord = 10;
    bool onState = true;
    float cordPosition;
    bool isScrolling;
    bool flippable;


    GameState LampState;
    //An enumerator to be sent to different events for event specific actions.
   

    //Activates once the mouse is scrolling over the object
    public void WasScrolled(float frameScrollValue)
    {
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
    }


    public void LightCheck()
    {
        
        switch (LampState)
        {
            case GameState.On:
                
                LampLight.gameObject.SetActive(true);
                break;
            case GameState.Off:
                
                LampLight.gameObject.SetActive(false);
                break;

        }
    }

    public void LightOn()
    {
        
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
