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
        if (frameScrollValue > 0)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.Up;
            return '.';
        }
        else if (frameScrollValue < 0)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.Down;
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
        
        if (frameScrollValue > 0)
        {
            Debug.Log("<color=green>Positive Scroll Value Recieved: " + frameScrollValue + " Dot</color>");
        }
        else if (frameScrollValue < 0)
        {
            Debug.Log("<color=red>Negative Scroll Value Recieved: " + frameScrollValue + " Dash</color>");
        }
        else
        {
            Debug.Log("<color=blue>Zero Scroll Value Recieved: " + frameScrollValue + " Space</color>");
           
        }



    }

}
