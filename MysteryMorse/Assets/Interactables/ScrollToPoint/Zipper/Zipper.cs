using UnityEngine;

public class Zipper : ScrollToPoint
{
    [SerializeField]
    Vector3 zipStartLocation;
    [SerializeField]
    Vector3 zipEndLocation;

    public GameObject zipperSlide;
    public GameObject note;
    private bool noteSpawned = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zipperSlide.transform.localPosition = zipStartLocation;
        slideValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (slideValue <= 0 && !noteSpawned)
        {
            Instantiate(note, transform.position, Quaternion.identity);
            noteSpawned = true;
        }
    }

    public override void UpdateVisuals()
    {
        Vector3 position = Vector3.Lerp(zipStartLocation, zipEndLocation, slideValue/targetValue);
        zipperSlide.transform.localPosition = position;
    }
}
