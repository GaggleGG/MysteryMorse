using Unity.VisualScripting;
using UnityEngine;

public class PosterScroll : ScrollToPoint
{
    [SerializeField] SpriteRenderer PosterBody;
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
        
        //adjust col transform
        transform.localScale = Vector3.Lerp(colStartSize, colEndSize, scalingCurve.Evaluate(slideValue/targetValue));
        transform.localPosition = Vector3.Lerp(colStartPos, colEndPos, scalingCurve.Evaluate(slideValue/targetValue));


        //adjust mask transform
        MaskTrans.localPosition = Vector3.Lerp(maskStartPos, maskEndPos, slideValue/targetValue);

        if(slideValue >=80)
        {
            //play rip audio here:


            //PosterBody.enabled = false;
            gameObject.SetActive(false);
        }

    }

    public override void UpdateVisuals()
    {
        
    }
}
