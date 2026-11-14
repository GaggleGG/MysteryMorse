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
    public float delayForSpace = 3f;

    [Header("Scroll Delay Timer")]
    [SerializeField]
    public float delaySeconds = 0.5f;

    [Header("Current Time Display")]
    public float timer = 0f;
    
    //This bool is used to determine if the player is currently scrolling or not.
    public bool isScrolling = false;


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

    
    public void UpdateMorseString(float frameScrollValue)
    {
         switch (currentScrollDirection)
        {
            case ScrollDirection.Up:
                currentMorseInput += ".";
                Debug.Log("<color=yellow>Add Dot to Current Morse Input: " + currentMorseInput + "</color>");
                break;
            case ScrollDirection.Down:
                currentMorseInput += "-";
                Debug.Log("<color=red>Add Dash to Current Morse Input: " + currentMorseInput + "</color>");
                break;
            case ScrollDirection.None:
                if (timer >= delayForSpace && previousScrollDirection != ScrollDirection.None)
                {
                    Debug.Log("<color=blue>Add Space to Current Morse Input: " + currentMorseInput + "</color>");
                    currentMorseInput += " ";
                }
                break;
            default:
                break;
        }
    }

    void Start()
    {
        ResetAll();
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
        if (frameScrollValue == 0 && timer >= delayForSpace && previousScrollDirection != ScrollDirection.None && currentScrollDirection != ScrollDirection.None)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.None;

            Debug.Log("<color=blue>Zero Scroll Value Recieved: " + frameScrollValue + " Space</color>");
            
            Debug.Log("<color=blue>Add Space to Current Morse Input: " + currentMorseInput + "</color>");
            currentMorseInput += " ";
            isScrolling = false;   
        }
    }

    //This function checks for a dot or dash based on the scroll value and updates the current morse input accordingly.
    public void CheckForDotOrDash(float frameScrollValue)
    {
        if (frameScrollValue < 0 && timer >= delaySeconds)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.Up;
            Debug.Log("<color=green>Positive Scroll Value Recieved: " + frameScrollValue + " Dot</color>");
            currentMorseInput += ".";
            Debug.Log("<color=yellow>Add Dot to Current Morse Input: " + currentMorseInput + "</color>");
            isScrolling = false;   
        }
        else if (frameScrollValue > 0 && timer >= delaySeconds)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.Down;
            Debug.Log("<color=red>Negative Scroll Value Recieved: " + frameScrollValue + " Dash</color>");
            currentMorseInput += "-";
            Debug.Log("<color=red>Add Dash to Current Morse Input: " + currentMorseInput + "</color>");
            isScrolling = false;
        }
    }

    

    
 void FixedUpdate()
    {
        if (!isScrolling)
        {
            ResetTimer();
            isScrolling = true;
        }
        else
        {
            timer += Time.fixedDeltaTime;
        } 
    }


    public void WasScrolled(float frameScrollValue)
    {
        //if you want to double chekc nly output when not 0  
        CheckForDotOrDash(frameScrollValue);
        CheckForSpace(frameScrollValue);
    }
}
