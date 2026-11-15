using Unity.VisualScripting;
using UnityEngine;

public class TuneScroll : CircularScroll
{
    float minStation = 55.1f;
    float maxStation = 155.1f;

    [SerializeField] AudioPlayer radioAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        radioAudio.StopAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void WasScrolled(float frameScrollValue)
    {
        base.WasScrolled(frameScrollValue);
    }

    
    void SetStation()
    {
        
    }


}
