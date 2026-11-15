using UnityEngine;

public class MorseInput : MonoBehaviour, IScrollable
{
    [Header("Input Switch Bone")]
    [SerializeField]
    private GameObject SwitchBone;
    
    [Header("Sound Effects")]
    [SerializeField]
    public AudioClip dotSound;
    [SerializeField]
    public AudioClip dashSound;
    [SerializeField]
    public AudioClip ClickSound;
    [SerializeField]
    public AudioSource audioSource;

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
    [SerializeField] private float restingZRotation = 90f;
    [SerializeField] private float maxZRotation = 115f;
    [SerializeField] private float minZRotation = 60f;
    [SerializeField] private float rotationSpeed = 15f; // Adjusted for smooth Lerp
    private float currentZRotation = 90f;
    private float targetZRotation = 90f;

     // Store starting local rotation angles
    private float initialYAngle;
    private float initialZAngle;
    private float currentXAngle = 0f;

    [Header("No Scroll Input Timer")]
    [SerializeField]
    public float delayForSpace = 1.5f;

    [Header("Scroll Delay Timer")]
    [SerializeField]
    public float delaySeconds = 0.25f;

    [Header("Current Time Display")]
    public float timer = 0f;
    private float inputCooldownTimer = 0f;
    private float idleTimer = 0f;
    
    //This bool is used to determine if the player is currently scrolling or not.
    public bool isScrolling = false;
    private bool canAddSpace = false;


    public void AnimateSwitchBone(float frameScrollValue)
    {
        if (frameScrollValue < 0)
        {
            LerpSwitchBoneRotation(maxZRotation);
        }
        else if (frameScrollValue > 0)
        {
            LerpSwitchBoneRotation(minZRotation);
        }
        
    }

    public void LerpSwitchBoneRotation(float targetZRotation)
    {
        currentZRotation = Mathf.Lerp(currentZRotation, targetZRotation, rotationSpeed * Time.deltaTime);
        SwitchBone.transform.localEulerAngles = new Vector3(SwitchBone.transform.localEulerAngles.x, SwitchBone.transform.localEulerAngles.y, currentZRotation);
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

    public void ClearMorseInput()
    {
        currentMorseInput = "";
    }

    public void CheckForSpace(float frameScrollValue)
    {
        if (frameScrollValue == 0 && timer >= delayForSpace && previousScrollDirection != ScrollDirection.None && currentScrollDirection != ScrollDirection.None && currentMorseInput.Length > 0)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.None;

            Debug.Log("<color=blue>Zero Scroll Value Recieved: " + frameScrollValue + " Space</color>");
            
            Debug.Log("<color=blue>Add Space to Current Morse Input: " + currentMorseInput + "</color>");
            currentMorseInput += " ";
            isScrolling = false;  
            ResetBoneRotation();

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
            audioSource.PlayOneShot(dotSound);
        }
        else if (frameScrollValue > 0 && timer >= delaySeconds)
        {
            previousScrollDirection = currentScrollDirection;
            currentScrollDirection = ScrollDirection.Down;
            Debug.Log("<color=red>Negative Scroll Value Recieved: " + frameScrollValue + " Dash</color>");
            currentMorseInput += "-";
            Debug.Log("<color=red>Add Dash to Current Morse Input: " + currentMorseInput + "</color>");
            isScrolling = false;
            audioSource.PlayOneShot(dashSound);
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
            idleTimer += Time.fixedDeltaTime;
        } 
        
    }


    public void WasScrolled(float frameScrollValue)
    {
        //if you want to double chekc nly output when not 0
        AnimateSwitchBone(frameScrollValue);  
        CheckForDotOrDash(frameScrollValue);
        //CheckForSpace(frameScrollValue);
        
    }
}
