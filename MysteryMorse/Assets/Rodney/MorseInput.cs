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

    private ScrollDirection currentScrollDirection = ScrollDirection.None;
    [SerializeField]
    private float zeroInputTimer = 3f;
    private ScrollDirection previousScrollDirection = ScrollDirection.None;


    public void AnimateSwitchBone(float frameScrollValue)
    {
        if (frameScrollValue > 0)
        {
            SwitchBone.transform.localRotation = Quaternion.Euler(0, 0, 45);
        }
        else if (frameScrollValue < 0)
        {
            SwitchBone.transform.localRotation = Quaternion.Euler(0, 0, -45);
        }
        else
        {
            SwitchBone.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
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
        else if (frameScrollValue == 0)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.None;
            
            while (zeroInputTimer < 3f)
            {
                zeroInputTimer += Time.deltaTime;
                
            }
            if (previousScrollDirection != currentScrollDirection)
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
            zeroInputTimer = 0f;
        }
        while (zeroInputTimer < 3f)
        {
            zeroInputTimer += Time.deltaTime;
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
