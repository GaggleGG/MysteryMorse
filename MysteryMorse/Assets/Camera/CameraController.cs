using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float rotSensitivity = 0.2f;

    private InputAction Scroll;
    private InputAction MiddleClick;


    private Vector2 mousePosition;

    private Camera mainCamera;    
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
