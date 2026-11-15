using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TuneScroll : CircularScroll
{
    float minStation = 55.4f;
    float maxStation = 155.4f;
    float station = 55.4f;
    public GameObject displayObj;
    public float secretStation;

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
        station = slideValue + 55.4f;
        displayObj.GetComponent<TextMeshPro>().text = station.ToString();
        SetStation();

    }

    
    void SetStation()
    {
        if (station == secretStation)
        {
            radioAudio.Play("Code");
            radioAudio.Stop("Static");
        }
        else
        {
            radioAudio.Play("Static");
            radioAudio.Stop("Code");
        }
    }


}
