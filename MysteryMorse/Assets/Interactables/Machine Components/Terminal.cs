using UnityEngine;
using Mono.Cecil.Cil;
using System.Collections.Generic;
using Unity.VisualScripting;


public class Morse 
{

    

    //Morse code Dictionary
   public static Dictionary<char, string> Code = new() {
        ['a'] = ".-",
        ['b'] = "-...",
        ['c'] = "-.-.",
        ['d'] = "-..",
        ['e'] = ".",
        ['f'] = "..-.",
        ['g'] = "--.",
        ['h'] = "....",
        ['i'] = "..",
        ['j'] = ".---",
        ['k'] = "-.-",
        ['l'] = ".-..",
        ['m'] = "--",
        ['n'] = "-.",
        ['o'] = "---",
        ['p'] = ".--.",
        ['q'] = "--.-",
        ['r'] = ".-.",
        ['s'] = "...",
        ['t'] = "-",
        ['u'] = "..-",
        ['v'] = "...-",
        ['w'] = ".--",
        ['x'] = "-..-",
        ['y'] = "-.--",
        ['z'] = "--..",
        ['0'] = "-----",
        ['1'] = ".----",
        ['2'] = "..---",
        ['3'] = "...--",
        ['4'] = "....-",
        ['5'] = ".....",
        ['6'] = "-....",
        ['7'] = "--...",
        ['8'] = "---..",
        ['9'] = "----."
    };

   public static Dictionary<string, char> Translation = new() {
        [".-"] = 'a',
        ["-..."] = 'b',
        ["-.-."] = 'c',
        ["-.."] = 'd',
        ["."] = 'e',
        ["..-."] = 'f',
        ["--."] = 'g',
        ["...."] = 'h',
        [".."] = 'i',
        [".---"] = 'j',
        ["-.-"] = 'k',
        [".-.."] = 'l',
        ["--"] = 'm',
        ["-."] = 'n',
        ["---"] = 'o',
        [".--."] = 'p',
        ["--.-"] = 'q',
        [".-."] = 'r',
        ["..."] = 's',
        ["-"] = 't',
        ["..-"] = 'u',
        ["...-"] = 'v',
        [".--"] = 'w',
        ["-..-"] = 'x',
        ["-.--"] = 'y',
        ["--.."] = 'z',
        ["-----"] = '0',
        [".----"] = '1',
        ["..---"] = '2',
        ["...--"] = '3',
        ["....-"] = '4',
        ["....."] = '5',
        ["-...."] = '6',
        ["--..."] = '7',
        ["---.."] = '8',
        ["----."] = '9'
    };


    //Solutions for the terminal
   public static Dictionary<string, int> Solutions = new()
    {
        [".-."] = 2,
        ["."] = 3,
        ["--."] = 4,
        ["---"] = 5,
        ["..."] = 6,
        ["..-."] = 7
    };




    public string TranslateMorse(string morseInput)
    {
        string[] morseWords = morseInput.Split(new string[] { "   " }, System.StringSplitOptions.None);
        string translatedText = "";

        foreach (string morseWord in morseWords)
        {
            string[] morseChars = morseWord.Split(' ');

            foreach (string morseChar in morseChars)
            {
                if (Translation.TryGetValue(morseChar, out char translatedChar))
                {
                    translatedText += translatedChar;
                }
                else
                {
                    translatedText += '?'; // Unknown character
                }
            }

            translatedText += ' '; // Space between words
        }

        return translatedText.Trim();
    }

    public void ValidateInput(string currentInput, Terminal terminal = null)
    {

        Debug.Log(currentInput);
        string tempInput = currentInput.Replace("?", "").Trim();
        
        Debug.Log(tempInput);
        if (Solutions.TryGetValue(tempInput, out int solution))
        {
            Debug.Log("<color=green>" + tempInput + " is valid! Solution: " + solution + "</color>");
            // print ticket
            
            

            if (terminal != null)
            {
                terminal.PrintTicket(solution);
                terminal.ChangeState(Terminal.TerminalState.CreateTicket);
            }
        }
        else
        {
            Debug.Log("<color=red>Input is invalid!</color>");
            //error sound
            if (terminal != null)
            {
                terminal.ChangeState(Terminal.TerminalState.WaitingForTicket);
            }
        }
    }   
}


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

    [SerializeField]
    public GameObject submitLever;

    [SerializeField]
    public GameObject printer;

    [SerializeField]
    public GameObject notePrefab;


    public enum TerminalState
    {
        CreateTicket,
        WaitingForTicket
    }

    public Ticket ticket = new Ticket();
    public Morse morseRef = new Morse();

    public string currentInput = "";
    public string currentTranslation = "";

    public TerminalState state = TerminalState.WaitingForTicket;

    
    public void ChangeState(TerminalState newState)
    {
        state = newState;
    }
   

    
    public void DisplayTranslation()
    {
        Debug.Log("<color=yellow>Current Translation: " + currentTranslation + "</color>");
    }

    public void ClearInput()
    {
        morseInput.GetComponent<MorseInput>().ClearMorseInput();
        currentInput = "";
        currentTranslation = "";
        state = TerminalState.WaitingForTicket;
        
    }




    [ContextMenu("Update Input")]
    public void UpdateInput()
    {
        if (morseInput == null)
        {
            Debug.LogError("<color=red>No Morse Input yet assigned!</color>");
        }
        else
        {
            currentInput = morseInput.GetComponent<MorseInput>().currentMorseInput;
            currentTranslation = morseRef.TranslateMorse(currentInput);
        }


        
    }


    void FixedUpdate()
    {
        UpdateInput();
        
        if (state == TerminalState.CreateTicket)
        {
            Debug.Log(ticket);
            printer.GetComponent<PrinterScript>().printTicket(ticket);
        }
    }


    [ContextMenu("Display Input")]
    public void DisplayInput()
    {
        Debug.Log("<color=yellow>Current Input: " + currentInput + "</color>");
    }


    //Grab the SubmitLver script state and call the ValidateInput method when the lever is pulled
    public void ValidateInput()
    {
        UpdateInput();
        Debug.Log("CHECKYYYY " + currentInput);
        string tempInput = currentInput;
        morseRef.ValidateInput(currentInput, this);
    }

    public void PrintTicket(int solutionID)
{
    Debug.Log(printer + " and " + notePrefab);
    if (notePrefab != null && printer != null)
    {
        GameObject spawnedTicket = Instantiate(notePrefab, printer.transform.position, Quaternion.identity);
        // spawnedTicket.GetComponent<Transform>().SetPositionAndRotation(new Vector3(-1.12f,1.2f,-4.18f),Quaternion.identity);
        Debug.Log("spawned ticket " + spawnedTicket + " " + solutionID);
        
        if (spawnedTicket.TryGetComponent<NotePickup>(out var note))
        {
            Debug.Log($"checkout this {note}");
            note.solutionID = solutionID;
        }
    }
    else
    {
        Debug.LogError($"<color=red>Missing reference on '{gameObject.name}'! NotePrefab is {(notePrefab == null ? "NULL" : "OK")}, Printer is {(printer == null ? "NULL" : "OK")}</color>", this);
    }
}

}
