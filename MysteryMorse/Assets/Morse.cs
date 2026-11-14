using Mono.Cecil.Cil;
using System.Collections.Generic;
using UnityEngine;
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

    //Solutions for the terminal
    static Dictionary<string, string> Solutions = new()
    {
        [""] = ""
    };
}
