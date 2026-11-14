using Unity.VisualScripting;
using UnityEngine;

public class PosterScroll : ScrollToPoint
{
    BoxCollider Col;

    //position of sprite mask obj
    Vector3 startPos;
    public Vector3 endPos;

    Vector3 colStartPos;
    public Vector3 colEndPos;    


    bool done = false; //poster is pulled all the way down

    Vector3 startSize;
    [SerializeField] Vector3 endSize;

    [SerializeField] AnimationCurve scalingCurve;


    Vector3 currentSize;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.localPosition;

        Col = GetComponent<BoxCollider>();
        startSize = Col.size;
        colStartPos = Col.center;

        Vector3 currentSize = Col.size;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void WasScrolled(float frameScrollValue)
    {
        base.WasScrolled(frameScrollValue);

        //update collider size
        //currentSize.Set(Col.size.x, Mathf.Lerp(startSize, endSize, slideValue/targetValue) , Col.size.z);
        currentSize = Vector3.Lerp(startSize, endSize, scalingCurve.Evaluate(slideValue/targetValue));
        Col.size = currentSize;

        Col.center = Vector3.Lerp(colStartPos, colEndPos, scalingCurve.Evaluate(slideValue/targetValue));



        //move position
        transform.localPosition = Vector3.Lerp(startPos, endPos, slideValue/targetValue);

    }

    public override void UpdateVisuals()
    {
        
    }
}
