using UnityEngine;

public class MorseInput : MonoBehaviour, IScrollable
{
    [Header("Input Switch Bone")]
    [SerializeField]
    private GameObject SwitchBone;

    public enum ScrollDirection
    {
        Up,
        Down,
        None
    };

    private ScrollDirection previousScrollDirection = ScrollDirection.None;
    private ScrollDirection currentScrollDirection = ScrollDirection.None;

    [Header("Current Morse Input")]

    [SerializeField]
    public string currentMorseInput = "";

    

    //switch bone rotation constraints
    [Header("Switch Bone Rotation Constraints")]
    [SerializeField]
    private float maxZRotation = 115f;
    [SerializeField]
    private float minZRotation = 60f;
    [SerializeField]
    private float currentZRotation = 90f;
    [SerializeField]
    private float rotationSpeed = 100f;

    [Header("No Scroll Input Timer")]
    [SerializeField]
    public float zeroInputTimer = 3f;

    public float timer = 0f;


    public void AnimateSwitchBone(float frameScrollValue)
    {
        if (frameScrollValue > 0)
        {
            LerpSwitchBoneRotation(maxZRotation);
        }
        else if (frameScrollValue < 0)
        {
            LerpSwitchBoneRotation(minZRotation);
        }
        else
        {
            LerpSwitchBoneRotation(currentZRotation);
        }
    }

    public void LerpSwitchBoneRotation(float targetZRotation)
    {
        currentZRotation = Mathf.Lerp(currentZRotation, targetZRotation, rotationSpeed * Time.deltaTime);
        SwitchBone.transform.localEulerAngles = new Vector3(SwitchBone.transform.localEulerAngles.x, SwitchBone.transform.localEulerAngles.y, currentZRotation);
    }

    
    public char ReturnDotOrDash(float frameScrollValue)
    {
    /*
        if (frameScrollValue > 0)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.Up;
            Debug.Log("<color=green>Positive Scroll Value Recieved: " + frameScrollValue + " Dot</color>");
            return '.';
        }
        else if (frameScrollValue < 0)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.Down;
            Debug.Log("<color=red>Negative Scroll Value Recieved: " + frameScrollValue + " Dash</color>");
            return '-';
        }
        // Return a space if no scroll for x amount of time. 
        // Only return space if the current scroll direction is different from the previous scroll direction. 
        // This is to prevent multiple spaces from being added when the player is not scrolling.
        else if (frameScrollValue == 0 && timer >= zeroInputTimer)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.None;
                        
               if (previousScrollDirection != ScrollDirection.None)
            {
                Debug.Log("<color=blue>Zero Scroll Value Recieved: " + frameScrollValue + " Space</color>");
                return ' ';
            } 
            
            
            else
            {
                return '\0'; // Return null character if no scroll and no change in direction
            }
        }
        else
        {
            // Return a space if no scroll for x amount of time.
            return '\0'; // Return null character if no scroll and no change in direction
        }
    */
        
        switch (currentScrollDirection)
        {
            case ScrollDirection.Up:
                currentMorseInput += ".";
                return '.';
            case ScrollDirection.Down:
                currentMorseInput += "-";
                return '-';
            case ScrollDirection.None:
                if (timer >= zeroInputTimer && previousScrollDirection != ScrollDirection.None)
                {
                    return ' ';
                }
                else
                {
                    return '\0'; // Return null character if no scroll and no change in direction
                }
            default:
                return '\0'; // Return null character if no scroll and no change in direction
        }

    }

    public void ResetScrollDirection()
    {
        previousScrollDirection = ScrollDirection.None;
        currentScrollDirection = ScrollDirection.None;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    public void ResetBoneRotation()
    {
        currentZRotation = 90f;
        SwitchBone.transform.localEulerAngles = new Vector3(SwitchBone.transform.localEulerAngles.x, SwitchBone.transform.localEulerAngles.y, currentZRotation);
    }
    
    public void ResetAll()
    {
        ResetScrollDirection();
        ResetTimer();
        ResetBoneRotation();
    }


    public void CheckForSpace(float frameScrollValue)
    {
        if (frameScrollValue == 0 && timer >= zeroInputTimer)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.None;

            if (previousScrollDirection != ScrollDirection.None)
            {
                Debug.Log("<color=blue>Zero Scroll Value Recieved: " + frameScrollValue + " Space</color>");
            }
        }
    }

    public void CheckForDotOrDash(float frameScrollValue)
    {
        if (frameScrollValue > 0)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.Up;
            Debug.Log("<color=green>Positive Scroll Value Recieved: " + frameScrollValue + " Dot</color>");
        }
        else if (frameScrollValue < 0)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.Down;
            Debug.Log("<color=red>Negative Scroll Value Recieved: " + frameScrollValue + " Dash</color>");
        }
    }

    void Update()
    {
        //Reset the scroll timer if the player is scrolling
        if (currentScrollDirection != ScrollDirection.None)
        {
            timer = 0f;
        }
        while (timer < zeroInputTimer)
        {
            timer += Time.deltaTime;
        }
    }




    //Animate the bone of input switch


    public void WasScrolled(float frameScrollValue)
    {
        //if you want to double chekc nly output when not 0
        
        CheckForDotOrDash(frameScrollValue);
        CheckForSpace(frameScrollValue);


    }

}
