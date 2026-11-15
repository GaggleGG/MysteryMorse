using UnityEngine;
using UnityEngine.UI;

//Note to be added to the book
public class NotePickup : ScrollToPoint
{
    Transform NoteModel;
    Vector3 startPos;
    public Vector3 endPos;
    bool collected = false;

    public int solutionID;

    public Image bookIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(bookIcon == null)
        {
            bookIcon = GameObject.Find("BookIcon").GetComponent<Image>();
        }

        NoteModel = transform.GetChild(0);
        if(startPos == null)
        {
            startPos = NoteModel.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public override void WasScrolled(float frameScrollValue)
    {
        base.WasScrolled(frameScrollValue);

        if(slideValue >= 100 && !collected)
        {
            collected = true;

            //Gets the book, then its script, and then calls the collect note method with the note's solution ID passed in
            bookIcon.GetComponent<ScrollToBook>().CollectNote(solutionID);
            Destroy(this.gameObject);
        }
    }

    public override void UpdateVisuals()
    {
        NoteModel.localPosition = Vector3.Lerp(startPos, endPos, slideValue/targetValue);
    }

}
