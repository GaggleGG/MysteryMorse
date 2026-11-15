using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
 
public class Lamp : ScrollToPoint
{
    [SerializeField] Light LampLight;
    [SerializeField] Transform CordTrans;
    Vector3 cordStartPos;
    [SerializeField] Vector3 cordEndPos;


    public delegate void LampToggled(bool lampOn);
    public static event LampToggled lampToggled;
    float prevSlideVal;

    public enum GameState
    {
        On,
        Off
    }


    //Amount pulled to flip lamp
    public float lightLockThreshhold = 90;

    bool locked = false;



    bool onState = true;
    //float cordPosition;
    bool isScrolling;
    bool flippable;

    bool scrollReceived = false; //did you scroll this frame


    //An enumerator to be sent to different events for event specific actions.
    GameState LampState;



    [SerializeField] int scrollingFrameWindow = 6;
    int scrollFrameTimer = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cordStartPos = CordTrans.localPosition;
        
        Debug.Log(cordStartPos);
        Debug.Log(cordEndPos);
    }
   

    //Activates once the mouse is scrolling over the object
    public override void WasScrolled(float frameScrollValue)
    {
        //only scroll one direction (pulling cord down)
        if(frameScrollValue > 0)
        {
            //Debug.Log("frame scroll val: " + frameScrollValue);
            base.WasScrolled(frameScrollValue);
            scrollFrameTimer = 0;


            if(!locked && slideValue == 100)
            {
                FlipLight();
                locked = true;
            }

            if(slideValue <= lightLockThreshhold)
            {
                locked = false;
            }

        }


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
        lampToggled?.Invoke(isOn);
        onState = isOn;
        LampLight.gameObject.SetActive(onState);
    }




    // Update is called once per frame
    void Update()
    {
        //set isscrolling state
        if(scrollFrameTimer <= scrollingFrameWindow)
        {
            //Debug.Log("Scrolling!");
            isScrolling = true;
            scrollFrameTimer++;
        }
        else
        {
            
            //Debug.Log("NOT SCROLLING!!");
            isScrolling = false;
        }

        if(!isScrolling)
        {
            slideValue = Mathf.MoveTowards(slideValue, 0, 300 * Time.deltaTime);
            //Debug.Log("slideval: " + slideValue);
        }

        CordTrans.localPosition = Vector3.Lerp(cordStartPos, cordEndPos, slideValue/targetValue);

    }
}
