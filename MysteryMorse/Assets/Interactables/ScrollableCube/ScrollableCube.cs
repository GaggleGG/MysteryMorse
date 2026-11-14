using UnityEngine;

public class ScrollableCube : MonoBehaviour, IScrollable
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WasScrolled(float frameScrollValue)
    {
        //if you want to double chekc nly output when not 0
        Debug.Log("Scroll Value Recieved: " + frameScrollValue);
    }
}
