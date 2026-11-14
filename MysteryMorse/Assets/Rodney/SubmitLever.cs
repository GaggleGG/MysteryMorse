using UnityEngine;

public class SubmitLever : MonoBehaviour, IScrollable
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

     [Header("Rotation Limits")]
    [SerializeField] private float minXRotation = -45f;
    [SerializeField] private float maxXRotation = 129f;
    [SerializeField] private float returnSpeed = 10f; // Speed for snapping back to 0
    [SerializeField] private float dragSensitivity = 0.5f;

     // Store starting local rotation angles
    private float initialYAngle;
    private float initialZAngle;
    private float currentXAngle = 0f;   

    //switch bone rotation constraints
    [Header("Switch Bone Rotation Constraints")]
    [SerializeField]
    private float maxZRotation = 130f;
    [SerializeField]
    private float minZRotation = 20f;
    
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
    
    public float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

    void Start()
    {
        //store base rot to restrict rot
        Vector3 initialEuler = transform.localEulerAngles;
        initialYAngle = initialEuler.y;
        initialZAngle = initialEuler.z;
        
        // Normalize starting X angle into -180 to 180 space
        currentXAngle = NormalizeAngle(initialEuler.x);
    }

    


    public void WasScrolled(float frameScrollValue)
    {
        //if you want to double chekc nly output when not 0  
        Debug.Log("<color=yellow>Submit Lever Scrolled: " + frameScrollValue + "</color>");
    }
}
