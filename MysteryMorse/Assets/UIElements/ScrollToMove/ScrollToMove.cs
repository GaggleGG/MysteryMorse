using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollToMove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //The event fuckery as mentioned in the camera controller
    public delegate void ScrollToMoveChanged(bool isHovered);
    public static ScrollToMoveChanged scrollToMoveChanged;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse Pointer Entered");
        scrollToMoveChanged?.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse Pointer Exited");
        scrollToMoveChanged?.Invoke(false);
    }
}
