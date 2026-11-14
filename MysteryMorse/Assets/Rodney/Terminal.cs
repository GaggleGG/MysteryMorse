using UnityEngine;
using Mono.Cecil.Cil;
using System.Collections.Generic;


public class Morse 
{
    //Morse code Dictionary
    static Dictionary<char, string> Code = new() {
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

    static Dictionary<string, char> Translation = new() {
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
    static Dictionary<string, int> Solutions = new()
    {
        ["--. --."] = 0
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
    public Morse morseRef = new Morse();

    public string currentInput = "";
    public string currentTranslation = "";

   

    
    public void DisplayTranslation()
    {
        Debug.Log("<color=yellow>Current Translation: " + currentTranslation + "</color>");
    }

    public void ClearInput()
    {
        currentInput = "";
        currentTranslation = "";
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
    }


    [ContextMenu("Display Input")]
    public void DisplayInput()
    {
        Debug.Log("<color=yellow>Current Input: " + currentInput + "</color>");
    }


}
