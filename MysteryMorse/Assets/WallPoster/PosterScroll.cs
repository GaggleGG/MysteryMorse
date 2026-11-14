using Unity.VisualScripting;
using UnityEngine;

public class PosterScroll : ScrollToPoint
{
    [SerializeField] Transform MaskTrans;
    //BoxCollider Col;

    //position of sprite mask obj
    Vector3 maskStartPos;
    public Vector3 maskEndPos;

    //positions of scrollable collider obj
    Vector3 colStartPos;

    public Vector3 colEndPos;


    bool done = false; //poster is pulled all the way down

    Vector3 colStartSize;
    [SerializeField] Vector3 colEndSize;

    [SerializeField] AnimationCurve scalingCurve;


    Vector3 currentSize;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colStartPos = transform.localPosition;

        colStartSize = transform.localScale;
        colStartPos = transform.localPosition;

        maskStartPos = MaskTrans.localPosition;


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
        //currentSize = Vector3.Lerp(startSize, endSize, scalingCurve.Evaluate(slideValue/targetValue));
        //Col.size = currentSize;
        
        //adjust col transform
        transform.localScale = Vector3.Lerp(colStartSize, colEndSize, scalingCurve.Evaluate(slideValue/targetValue));
        transform.localPosition = Vector3.Lerp(colStartPos, colEndPos, scalingCurve.Evaluate(slideValue/targetValue));


        //adjust mask transform
        MaskTrans.localPosition = Vector3.Lerp(maskStartPos, maskEndPos, scalingCurve.Evaluate(slideValue/targetValue));

        //move position

    }

    public override void UpdateVisuals()
    {
        
    }
}
