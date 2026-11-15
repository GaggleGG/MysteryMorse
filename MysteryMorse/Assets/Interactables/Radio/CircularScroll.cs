using Unity.Mathematics;
using UnityEngine;

public class CircularScroll : MonoBehaviour, IScrollable
{
    protected float slideValue = 0;
    protected float targetValue = 100;

    public float increaseMod = 5;
    public float startAngle;
    Vector3 rotationAxis;

    /*
    Vector3 startRotation;
    public Vector3 endRotation;
    */
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slideValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void WasScrolled(float frameScrollValue)
    {
        slideValue += frameScrollValue * increaseMod;
        if(slideValue < 0)
            slideValue = 100;
        if(slideValue >= 100)
        {
            slideValue = 0;
        }



        UpdateVisuals();        

    }

    public virtual void UpdateVisuals()
    {
        transform.rotation = Quaternion.AngleAxis(startAngle + (360 * (slideValue/targetValue)), rotationAxis);
        
    }
}
