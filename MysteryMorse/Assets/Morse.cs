
using System.Collections.Generic;

public class Morseus 
{
    public string morse = "Morsin Time";
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
        [""] = 0
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
