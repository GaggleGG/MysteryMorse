using UnityEngine;

public class VolumeScroll : CircularScroll
{

    [SerializeField] AudioPlayer radioAudio;

    private void Start()
    {
        slideValue = 50;
    }

    private void Update()
    {
        
    }

    public override void WasScrolled(float frameScrollValue)
    {
        base.WasScrolled(frameScrollValue);
        foreach (Sound s in radioAudio.sounds)
        {
            s.source.volume = slideValue/100;
        }
        Debug.Log("Volume slide value" + slideValue);
    }
}
