using UnityEngine;

public class ScrollToPoint : MonoBehaviour, IScrollable
{
    protected float slideValue = 0;
    protected float targetValue = 100;

    float increaseMod = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void WasScrolled(float frameScrollValue)
    {
        slideValue += frameScrollValue * increaseMod;
        if(slideValue < 0)
            slideValue = 0;
        if(slideValue >= 100)
        {
            slideValue = 100;
        }

        UpdateVisuals();
    }

    public virtual void UpdateVisuals()
    {
        
    }

}
