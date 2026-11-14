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
    





    [ContextMenu("Update Input")]
    public void UpdateInput()
    {
        currentInput = morseInput.GetComponent<MorseInput>().currentMorseInput;
    }


    [ContextMenu("Clear Input")]
    public void ClearInput()
    {
        currentInput = "";
    }

    [ContextMenu("Display Input")]
    public void DisplayInput()
    {
        Debug.Log("<color=yellow>Current Input: " + currentInput + "</color>");
    }


}
