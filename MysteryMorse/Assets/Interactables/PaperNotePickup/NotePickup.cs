using UnityEngine;

//Note to be added to the book
public class NotePickup : ScrollToPoint
{
    Transform NoteModel;
    Vector3 startPos;
    public Vector3 endPos;
    bool collected = false;
    


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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


            //add note to book inventory
            Debug.Log("Collect Note!");

        }
    }

    public override void UpdateVisuals()
    {
        NoteModel.localPosition = Vector3.Lerp(startPos, endPos, slideValue/targetValue);
    }

}
