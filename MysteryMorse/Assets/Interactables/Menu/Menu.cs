using UnityEngine;
using UnityEngine.UI;

public class Menu : ScrollToPoint
{
    public GameObject menu;
    //public GameObject background;
    [SerializeField]
    Vector3 menuStart;
    [SerializeField]
    Vector3 menuEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //destroy if slider value is 100
        if (slideValue == 100)
        {
            //Destroy(background);
            Destroy(gameObject);
        }
    }

    public override void UpdateVisuals()
    {
        Vector3 position = Vector3.Lerp(menuStart, menuEnd, slideValue / targetValue);
        menu.transform.localPosition = position;
       // background.transform.localPosition = position;
    }
}
