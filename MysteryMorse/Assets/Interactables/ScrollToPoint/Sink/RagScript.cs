using UnityEngine;

public class RagScript : MonoBehaviour, IScrollable
{
    public SinkScript sinkScript;
    bool canBeWiped = false;

    float currentWipe = 0;
    float wipeThreshold = 100;
    public void WasScrolled(float frameScrollValue)
    {
        float absFrame = Mathf.Abs(frameScrollValue);
        //Do any animation stuff here

        //Don't actually wipe unless the sink can be wiped
        if(canBeWiped && absFrame != 0)
        {
            currentWipe += absFrame;
            if(currentWipe > wipeThreshold)
                currentWipe = wipeThreshold;
            float wipePercent = (currentWipe/wipeThreshold);
            sinkScript.MirriorWiped(wipePercent);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SinkScript.sinkFullyOn += SinkTurnedOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SinkTurnedOn(bool sinkOn)
    {
        if(sinkOn && !canBeWiped)
        {
            canBeWiped = true;
        }
    }
}
