using System.Collections;
using System.Collections.Generic;
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

    //List of book elements to collect
    public List<Image> bookElements;

    //List of game objects already unlocked
    private bool[] unlockedElements = new bool[12];

    //Current page index
    private int pageIndex = 0;

    //Amount of pages the player currently has
    private int unlockedPages = 1;

    //Bool that determines if the book is being scrolled over
    private bool useScrollBook;

    //Bool that determines if the book is open or not, instantiated to false
    private bool bookOpen = false;

    //Scrollable value, which determines if the book can be interacted with or not
    private bool scrollable = false;

    //Book positions
    private Vector2 bookCenter = new Vector2(0, 0);
    private Vector2 bookOffScreen = new Vector2(0, -1100);

    //Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        //Scroll input action
        Scroll = InputSystem.actions.FindAction("Scroll");

        //Mouse position vector
        mousePosition = Vector2.zero;

        //Using the designated script, listens for the scroll input
        scrollToBookChanged += ScrollBookHovered;

        //Sets the alphabet and tutorial to active at the start
        unlockedElements[0] = true;
        unlockedElements[1] = true;
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
                //Starts a coroutine to smoothly move the book up
                StartCoroutine(MoveBook(bookOffScreen, bookCenter, 2));
                
                //Changes the book open state and sets scrollable to false
                bookOpen = true;
            //If the player is hovering the scroll book and the book is open
            } else if (useScrollBook && bookOpen == true) {
                //Starts a coroutine to smoothly move the book down
                StartCoroutine(MoveBook(bookCenter, bookOffScreen, 2));      

                //Changes the book open state and sets scrollable to false
                bookOpen = false;
            //If the player is not hovering the book icon and has the book open, they can flip pages
            } else if (!useScrollBook && bookOpen == true) {
                //Flip pages left
                if (scrollValue.y > 0) {
                    StartCoroutine(FlipToPage(pageIndex - 1));
                //Flip pages right
                } else if (scrollValue.y < 0) {
                    StartCoroutine(FlipToPage(pageIndex + 1));
                }
            }
        }
    }

    //Method that is called by notes to activate corresponding elements in the book
    public void CollectNote(int solutionID) {
        //Sets the book UI to active
        setUIActive(book);
        //Sets the corresponding element active
        setUIActive(bookElements[solutionID]);
        //Sets the corresponding element to unlocked
        unlockedElements[solutionID] = true;

        //Forces the page index to be 1 and open the book
        StartCoroutine(FlipToPage(1));

        //Starts a coroutine to smoothly move the book up
        StartCoroutine(MoveBook(bookOffScreen, bookCenter, 2));

        //Sets book open to true
        bookOpen = true;
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
    void setUIActive(Image UIObject) {
        UIObject.gameObject.SetActive(true);
    }

    //Reusable methods for disabling any UI elements, overloaded for different UI element types
    void setUIInactive(Image UIObject) {
        UIObject.gameObject.SetActive(false);
    }

    //Method that animates the book while it moves position
    IEnumerator MoveBook(Vector2 from, Vector2 to, float speed) {
        //Ensures the player can't rapidly do the action
        scrollable = false;

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

        //Resets scrollable when the animation is done
        scrollable = true;
    }

    //Function that flips the book the specific page, revealing its elements and hiding the others
    IEnumerator FlipToPage(int pageNumber) {
        //Changes the page index to the page number, but clamped to a range
        pageIndex = Mathf.Clamp(pageNumber, 0, unlockedPages);

        //Removes every UI element
        foreach (Image image in bookElements) {
            setUIInactive(image);
        }

        //Switch statement that reveals things on the page based on what is on the page and what the player has unlocked
        switch (pageIndex) {
            //First page
            case 0:
                //Alphabet/Tutorial
                if (unlockedElements[0] == true) {
                    setUIActive(bookElements[0]);
                }
                if (unlockedElements[1] == true) {
                    setUIActive(bookElements[1]);
                }

                //Page numbers
                setUIActive(bookElements[8]);
                setUIActive(bookElements[9]);
                break;
            //Second page
            case 1:
                //Tickets
                if (unlockedElements[2] == true) {
                    setUIActive(bookElements[2]);
                }
                if (unlockedElements[3] == true) {
                    setUIActive(bookElements[3]);
                }
                if (unlockedElements[4] == true) {
                    setUIActive(bookElements[4]);
                }
                if (unlockedElements[5] == true) {
                    setUIActive(bookElements[5]);
                }
                if (unlockedElements[6] == true) {
                    setUIActive(bookElements[6]);
                }
                if (unlockedElements[7] == true) {
                    setUIActive(bookElements[7]);
                }

                //Page numbersS
                setUIActive(bookElements[10]);
                setUIActive(bookElements[11]);
                break;
            default:
                break;
        }

        yield return null;
    }
}
