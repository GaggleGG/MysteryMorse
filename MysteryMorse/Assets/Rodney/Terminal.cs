using UnityEngine;

public class Terminal : MonoBehaviour
{
    /// <summary>
    /// 
    /// This class takes the input from the player and processes it. 
    /// The procsessed output is then sent to the UI to be displayed to the player. 
    /// live translation is also done here.
    /// 
    /// </summary>

    [SerializeField]
    public GameObject morseInput;
    private Morse morseRef;

    private string currentInput = "";


    private void LiveTranslate(string input)
    {
        Debug.Log("Current Input: " + input);
        //morseRef.TranslateInput(input);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    [ContextMenu("Update Input")]
    public void UpdateInput()
    {
        if (morseInput.ReturnDotorDash() == ".")
        {
            currentInput += ".";
            LiveTranslate(currentInput);
        }
        else if (morseInput.ReturnDotOrDash() == "-")
        {
            currentInput += "-";
            LiveTranslate(currentInput);
        }
        else if (morseInput.ReturnDotOrDash() == " ")
        {

            //Ignore the space input if the current input is empty
            if (currentInput.Length == 0)
            {
                return;
            }
            //ignore the space input if the current input is already a space
            if (currentInput[currentInput.Length - 1] == ' ')
            {
                return;
            }

            currentInput += " ";
            LiveTranslate(currentInput);
        }
    }


    [ContextMenu("Clear Input")]
    public void ClearInput()
    {
        currentInput = "";
        LiveTranslate(currentInput);
    }

    [ContextMenu("Display Input")]
    public void DisplayInput()
    {
        Debug.Log("<color=yellow>Current Input: " + currentInput + "</color>");
    }


}
