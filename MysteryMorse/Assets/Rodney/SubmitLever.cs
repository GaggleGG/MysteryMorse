using UnityEngine;

public class SubmitLever : MonoBehaviour, IScrollable
{


    [Header("Input Switch Bone")]
    [SerializeField]
    private GameObject LeverBone;

    [Header("Terminal Reference")]
    
    private Terminal terminalRef;

    [SerializeField]
    private GameObject terminal;



    [Header("Sound Effects")]
    [SerializeField] public AudioClip ClickSound;
    [SerializeField] public AudioSource audioSource;

    public enum LeverState
    {
        isPulled,
        isNotPulled
    }
    public LeverState currentState = LeverState.isNotPulled;

    [Header("Switch Bone Rotation Constraints")]
    [SerializeField] private float maxZRotation = 130f;
    [SerializeField] private float minZRotation = 20f;
    [SerializeField] private float scrollSensitivity = 5f; // How far each scroll step moves the lever
    [SerializeField] private float rotationSpeed = 15f;    // Smoothing speed toward target Z angle
    [SerializeField] private float returnSpeed = 10f;      // Speed for snapping back when idle
    private Quaternion initialLocalRotation;

    [Header("Scroll & Cooldown Timers")]
    [SerializeField] public float delayForSpace = 3f;
    [SerializeField] public float delaySeconds = 0.5f;   // Cooldown to prevent submit spam
    [SerializeField] private float returnDelay = 0.25f;    // Wait time after scrolling stops before snapping back
    [SerializeField] private int scrollingFrameWindow = 4;

    [Header("Current Time Display")]
    public float timer = 0f;
    public bool isScrolling = false;

    private float initialXAngle;
    private float initialYAngle;
    private float currentZAngle;
    private float targetZAngle;

    private int scrollFrameTimer = 0;
    private float timeSinceLastScroll = 0f;
    private float submitCooldownTimer = 0f;


    public float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

    void Start()
    {
        terminalRef = terminal.GetComponent<Terminal>();

        //store base rot to restrict rot
        Transform targetTransform = LeverBone != null ? LeverBone.transform : transform;
        Vector3 initialEuler = targetTransform.localEulerAngles;
        initialYAngle = initialEuler.y;
        initialXAngle = initialEuler.x;
        initialLocalRotation = targetTransform.localRotation;
        
        currentZAngle = minZRotation;
        targetZAngle = minZRotation;
        ApplyZRotation(currentZAngle);
    }

   void Update()
   {
        if (submitCooldownTimer > 0f)
        {
            submitCooldownTimer -= Time.deltaTime;
        }

        if (!isScrolling)
        {
            timeSinceLastScroll += Time.deltaTime;

            // Once enough time passes without scroll input, lerp target back to minZRotation
            if (timeSinceLastScroll >= returnDelay)
            {
                targetZAngle = Mathf.Lerp(targetZAngle, minZRotation, Time.deltaTime * returnSpeed);
                if (Mathf.Abs(targetZAngle - minZRotation) < 0.05f)
                {
                    targetZAngle = minZRotation;
                    currentState = LeverState.isNotPulled;
                }
            }
        }

        // Smoothly interpolate current Z angle toward target Z angle
        if (!Mathf.Approximately(currentZAngle, targetZAngle))
        {
            currentZAngle = Mathf.Lerp(currentZAngle, targetZAngle, Time.deltaTime * rotationSpeed);

            if (Mathf.Abs(currentZAngle - targetZAngle) < 0.05f)
            {
                currentZAngle = targetZAngle;
            }

            ApplyZRotation(currentZAngle);
        }

        CheckSubmitThreshold();
    }



    //when the player scrolls over the lever, check if they are scrolling up or down and move the lever accordingly. If the lever is pulled all the way down, it will trigger the submit action.
    // maintain a timer to prevent the player from spamming the lever and submitting multiple times in a row. 
    // The lever will also Lerp back to its original position after a certain amount of time has passed without any input.
    void FixedUpdate()
    {
        if (isScrolling)
        {
            scrollFrameTimer++;
            if (scrollFrameTimer >= scrollingFrameWindow)
            {
                isScrolling = false;
                scrollFrameTimer = 0;
            }
        }
    }

    

    public void WasScrolled(float frameScrollValue)
    {
        //if you want to double chekc nly output when not 0  
       if (Mathf.Approximately(frameScrollValue, 0f)) return;

        isScrolling = true;
        scrollFrameTimer = 0;
        timeSinceLastScroll = 0f;

        // Adjust target Z angle by scroll delta (flip -= to += if scroll direction feels inverted)
        targetZAngle -= frameScrollValue * scrollSensitivity;
        targetZAngle = Mathf.Clamp(targetZAngle, minZRotation, maxZRotation);
    }

    private void CheckSubmitThreshold()
    {
        // Trigger submit when pulled nearly all the way to maxZRotation and cooldown is ready
        if (currentState == LeverState.isNotPulled &&
            currentZAngle >= maxZRotation - 1f &&
            submitCooldownTimer <= 0f)
        {
            currentState = LeverState.isPulled;
            submitCooldownTimer = delaySeconds;
            TriggerSubmit();
        }
    }

    private void TriggerSubmit()
    {
       
        Debug.Log("<color=green>Submit Lever Pulled!</color>");
        if (terminalRef != null)
        {
            terminalRef.ValidateInput();
            //terminalRef.UpdateInput();
            //terminalRef.DisplayTranslation();
            // yield return new WaitForSecondsRealtime(0.1f);
            terminalRef.ClearInput();
        }
        else
        {
            Debug.LogError("<color=red>No Terminal Reference Assigned!</color>");
        }

        
    }
    private void ApplyZRotation(float zAngle)
    {
        Transform targetTransform = LeverBone != null ? LeverBone.transform : transform;
        // Rotates cleanly around the bone's own local Z-axis relative to its starting pose
        targetTransform.localRotation = initialLocalRotation * Quaternion.Euler(0f, 0f, zAngle);
    }
}
