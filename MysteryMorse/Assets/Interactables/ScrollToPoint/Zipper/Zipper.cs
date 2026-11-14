using UnityEngine;

public class Zipper : ScrollToPoint
{
    [SerializeField]
    Vector3 zipStartLocation;
    [SerializeField]
    Vector3 zipEndLocation;

    public GameObject zipperSlide;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zipperSlide.transform.localPosition = zipStartLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void UpdateVisuals()
    {
        Vector3 position = Vector3.Lerp(zipStartLocation, zipEndLocation, slideValue/targetValue);
        zipperSlide.transform.localPosition = position;
    }
}
