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



/*
    [ContextMenu("Update Input")]
    //public void UpdateInput()
    //{
    //    if (morseInput.ReturnDotorDash() == ".")
    //    {
    //        currentInput += ".";
    //        LiveTranslate(currentInput);
    //    }
    //    else if (morseInput.ReturnDotOrDash() == "-")
    //    {
    //        currentInput += "-";
    //        LiveTranslate(currentInput);
    //    }
    //    else if (morseInput.ReturnDotOrDash() == " ")
    //    {

    //        //Ignore the space input if the current input is empty
    //        if (currentInput.Length == 0)
    //        {
    //            return;
    //        }
    //        //ignore the space input if the current input is already a space
    //        if (currentInput[currentInput.Length - 1] == ' ')
    //        {
    //            return;
    //        }

<<<<<<< HEAD
            currentInput += " ";
            LiveTranslate(currentInput);
        }
    }
*/
=======
    //        currentInput += " ";
    //        LiveTranslate(currentInput);
    //    }
    //}

>>>>>>> cc04780cae67c941627f1452092737ae2924b869

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
