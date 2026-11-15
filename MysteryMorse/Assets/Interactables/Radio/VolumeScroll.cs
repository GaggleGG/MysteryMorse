using UnityEngine;

public class VolumeScroll : CircularScroll
{

    [SerializeField] AudioPlayer radioAudio;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public override void WasScrolled(float frameScrollValue)
    {
        base.WasScrolled(frameScrollValue);
        foreach (Sound s in radioAudio.sounds)
        {
            s.volume = slideValue;
        }
    }
}
