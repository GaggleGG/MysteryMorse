using UnityEngine;

public class SinkScript : ScrollToPoint
{

    float waterBreakpoint = 25;
    float smallSteamBreakpoint = 75;



    public GameObject Tap;
    public Vector3 StartTapRotation;
    public Vector3 EndTapRotation;
    public ParticleSystem Steam;
    public ParticleSystem TapWater;


    public delegate void SinkFullyOnChanged(bool sinkFullyOn);
    public static event SinkFullyOnChanged sinkFullyOn;
    bool sinkOn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        increaseMod = 1f;
        Steam.Stop();
        TapWater.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FullyOn()
    {
        
    }

    public override void UpdateVisuals()
    {
        Vector3 tapRotation = Vector3.Lerp(StartTapRotation, EndTapRotation, slideValue/targetValue);
        Tap.transform.localEulerAngles = tapRotation;

        if(slideValue < waterBreakpoint)
        {
            if(TapWater.isPlaying)
            {
                TapWater.Stop();
            }
        }
        else
        {
            if(!TapWater.isPlaying)
            {
                TapWater.Play();
            }

        }

        if(slideValue < smallSteamBreakpoint)
        {
            if(Steam.isPlaying)
            Steam.Stop();
        }
        else
        {
            if(!Steam.isPlaying)
            Steam.Play();
        }
        if(slideValue > 99.9f)
        {
            sinkFullyOn?.Invoke(true);
            sinkOn = true;
        }
        else
        {
            if(sinkOn)
            {
                sinkFullyOn?.Invoke(false);
                sinkOn = false;
            }
        }
    }
}
