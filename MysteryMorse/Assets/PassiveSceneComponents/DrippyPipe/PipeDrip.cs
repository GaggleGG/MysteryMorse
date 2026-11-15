using UnityEngine;

public class PipeDrip : MonoBehaviour
{
    
    private AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //Set to loop
        audioSource.loop = true;
        this.enabled = false;

        SinkScript.sinkWaterRunning += ToggleDrip;
    }

    private void OnDestroy()
    {
        SinkScript.sinkWaterRunning -= ToggleDrip;
    }

    void ToggleDrip(bool dripActive)
    {
        if(dripActive)
        {
            Debug.Log("Sink Turned On");
            audioSource.Play();
        }
        else
        {
            //if you want the source to resume from where it was use Pause, this should reset it every time
            audioSource.Stop();
        }
    }
}
