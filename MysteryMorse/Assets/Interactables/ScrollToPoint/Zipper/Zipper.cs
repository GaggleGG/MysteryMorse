using UnityEngine;

public class Zipper : ScrollToPoint
{
    [SerializeField]
    Vector3 zipStartLocation;
    [SerializeField]
    Vector3 zipEndLocation;

    public GameObject zipperSlide;
    public GameObject Tag;
    private bool unZipped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tag.SetActive(false);

        zipperSlide.transform.localPosition = zipStartLocation;
        slideValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("slideValue: " + slideValue);

        if (slideValue >= 100 && !unZipped)
        {
            UnZip();
            
        }
    }
    public override void WasScrolled(float frameScrollValue)
    {
        if(!unZipped)
        {
            base.WasScrolled(frameScrollValue);
        }
    }

    public override void UpdateVisuals()
    {
        zipperSlide.transform.localPosition = Vector3.Lerp(zipStartLocation, zipEndLocation, slideValue/targetValue);
    }

    void UnZip()
    {
        unZipped = true;

        //set jacket model to unzipped

        //reveal tag
        Tag.SetActive(true);

    }

}
