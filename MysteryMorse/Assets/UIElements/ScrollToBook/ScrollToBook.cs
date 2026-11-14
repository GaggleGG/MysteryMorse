using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScrollToBook : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    //Hover event
    public delegate void ScrollToBookChanged(bool isHovered);
    public static ScrollToBookChanged scrollToBookChanged;

    //Scroll input action
    private InputAction Scroll;

    //Mouse position
    private Vector2 mousePosition;

    //Actual book UI element
    [SerializeField] private Image book;

    //Bool that determines if the book is being scrolled over
    private bool useScrollBook;

    //Bool that determines if the book is open or not, instantiated to false
    private bool bookOpen = false;

    //Scrollable value, which determines if the book can be interacted with or not
    private bool scrollable = false;

    //Book positions
    private Vector2 bookCenter = new Vector2(0, 0);
    private Vector2 bookOffScreen = new Vector2(0, -1000);

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //Scroll input action
        Scroll = InputSystem.actions.FindAction("Scroll");

        //Mouse position vector
        mousePosition = Vector2.zero;

        //Using the designated script, listens for the scroll input
        scrollToBookChanged += ScrollBookHovered;
    }

    //Update is called once per frame
    void Update() {
        //Reads the current mouse and scroll wheel valuesS
        mousePosition = Mouse.current.position.ReadValue();
        Vector2 scrollValue = Scroll.ReadValue<Vector2>();
        
        /* Checks if the scroll value has changed from from zero (e.g. the player is actively scrolling),
         * and if the scrollable value is true, which ensures the book does not spam open and close. Opening
         * or closing the book sets scrollable to false, and can only ever be set to true again by re-hovering
         * the book. */
        if (scrollValue != Vector2.zero && scrollable == true) {
            //If the player is hovering the scroll book and the book is not open
            if (useScrollBook && bookOpen == false) {
                //Sets the book UI to active
                setUIActive(book);
                scrollable = false;
                //Starts a coroutine to smoothly move the book up
                StartCoroutine(MoveBook(bookOffScreen, bookCenter, 2));
                
                //Changes the book open state and sets scrollable to false
                bookOpen = true;
            //If the player is hovering the scroll book and the book is open
            } else if (useScrollBook && bookOpen == true) {
                scrollable = false;
                //Starts a coroutine to smoothly move the book down
                StartCoroutine(MoveBook(bookCenter, bookOffScreen, 2));      

                //Changes the book open state and sets scrollable to false
                bookOpen = false;
            }
        }
    }

    //Sets the using book variable to true when the player scrolls over the specific UI element
    void ScrollBookHovered(bool useScrollBook) {
        this.useScrollBook = useScrollBook;
    }

    //Event that fires when the player hovers the book UI
    public void OnPointerEnter(PointerEventData eventData) {
        //Invokes the change
        scrollToBookChanged?.Invoke(true);

        //Sets scrollable to true only here. This means that it fires once when the player hovers the book
        scrollable = true;
    }

    //Event that fires when the player stops hovering the book UI
    public void OnPointerExit(PointerEventData eventData) {
        //Invokes the change
        scrollToBookChanged?.Invoke(false);

        //Sets scrollable to false to prevent storing this value when hovering the book and not opening or closing it
        scrollable = false;
    }

    //Reusable methods for setting any UI elements to active, overloaded for different UI element types
    void setUIActive(TMP_Text UIObject) {
        UIObject.gameObject.SetActive(true);
    }

    void setUIActive(Image UIObject) {
        UIObject.gameObject.SetActive(true);
    }

    //Reusable methods for disabling any UI elements, overloaded for different UI element types
    void setUIInactive(TMP_Text UIObject) {
        UIObject.gameObject.SetActive(false);
    }

    void setUIInactive(Image UIObject) {
        UIObject.gameObject.SetActive(false);
    }

    //Method that animates the book while it moves position
    IEnumerator MoveBook(Vector2 from, Vector2 to, float speed) {
        //Interval
        var t = 0f;

        //While it has not reached the interval, lerps the book
        while (t <= 1f) {
            t += speed * Time.deltaTime;
            book.rectTransform.anchoredPosition = Vector2.Lerp(from, to, t);
            yield return null;
        }

        //Sets the true position of the book at the end of the coroutine based on where it is being moved
        if (to == bookCenter) {
            book.rectTransform.anchoredPosition = bookCenter;
        } else {
            book.rectTransform.anchoredPosition = bookOffScreen;
            setUIInactive(book);
        }

        scrollable = true;
    }
}
