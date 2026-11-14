using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float rotSensitivity = 1f;

    //InputActions
    private InputAction Scroll;
    private InputAction MiddleClick;
    //we may not need the mouse position but its here
    private Vector2 mousePosition;



    private bool useScrollCamera;
    private Camera mainCamera;    
    void Start()
    {
        mainCamera = Camera.main;
        Scroll = InputSystem.actions.FindAction("Scroll");
        MiddleClick = InputSystem.actions.FindAction("MiddleClick");

        //unlock cursor to mess with it
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        mousePosition = Vector2.zero;
        ScrollToMove.scrollToMoveChanged += ScrollCameraHovered;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        Vector2 scrollValue = Scroll.ReadValue<Vector2>();
        if(useScrollCamera)
        {
            mainCamera.transform.localRotation = Quaternion.Euler(mainCamera.transform.localEulerAngles.x,
            mainCamera.transform.localEulerAngles.y + (scrollValue.y * rotSensitivity),
            mainCamera.transform.localEulerAngles.z);
        }
        else
        {
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.TryGetComponent<IScrollable>(out IScrollable scrollableObject))
                {
                    scrollableObject.WasScrolled(scrollValue.y);
                }
            }
        }
    }


    //handlers for dealing with enter and exit for the mouse over the camera move area
    //Theres no .hovered property unfortunately so we gotta do some event fuckery.
    void ScrollCameraHovered(bool useScrollCamera)
    {
        this.useScrollCamera = useScrollCamera;
    }
}
