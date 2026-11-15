using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LightController : MonoBehaviour
{
    public Light flashLight;
    bool lightIsFlashing;
    [SerializeField]
    private string message = "waddup";


    [SerializeField]
    //turn this up if you want the pattern to take a bit longer
    private float durationMultiplier = 1f;

    //Adjust these to get the pattern looking like you want
    [SerializeField]
    float dotDuration = 0.2f;
    [SerializeField]
    float dashDuration = 0.4f;
    [SerializeField]
    float letterSpaceDuration = 0.4f;
    [SerializeField]
    float wordSpaceDuration = 0.6f;

    Coroutine lightCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //lightIsFlashing = false;
        //flashLight.enabled = false;

        lightIsFlashing = true;
        StartPlayingMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPlayingMessage()
    {
        //the dictionary is all lowercase so I'll just doa bit of error checking
        lightCoroutine = StartCoroutine(FlashMessage(message.ToLower()));
    }

    public void StopPlayingMessage()
    {
        StopCoroutine(lightCoroutine);
    }

    private IEnumerator FlashMessage(string text)
    {
        do
        {
            string[] words = text.Split(' ');

            for (int w = 0; w < words.Length; w++)
            {
                string word = words[w];

                for (int c = 0; c < word.Length; c++)
                {
                    if (!Morse.Code.ContainsKey(word[c]))
                    {
                        //Ignore it if someone adds a symbol we haven't accounted for
                        continue;
                    }
                    string code = Morse.Code[word[c]];
                    for (int s = 0; s < code.Length; s++)
                    {
                        ToggleLight(true);
                        if(code[s] == '.')
                        {
                            yield return new WaitForSeconds(dotDuration * durationMultiplier);
                        }
                        else
                        {
                            yield return new WaitForSeconds(dashDuration * durationMultiplier);
                        }

                        ToggleLight(false);

                        if (s < code.Length - 1)
                        {
                            yield return new WaitForSeconds(dotDuration);
                        }
                    }

                    if (c < word.Length - 1)
                    {
                        yield return new WaitForSeconds(letterSpaceDuration * durationMultiplier);
                    }
                }

                if (w < words.Length - 1)
                {
                    yield return new WaitForSeconds(wordSpaceDuration * durationMultiplier);
                }
            }
            if (lightIsFlashing)
            {
                yield return new WaitForSeconds(wordSpaceDuration * durationMultiplier);
            }

        } while (lightIsFlashing);
    }

    //broke this out into a function so if we have an emissive material we want to toggle or soemthing do it here, it'll update with the light
    private void ToggleLight(bool lightOn)
    {
        if(lightOn)
        {
            //also potential spot for an audio queue
            flashLight.enabled = true;
        }
        else
        {
            flashLight.enabled = false;
        }
    }
}
