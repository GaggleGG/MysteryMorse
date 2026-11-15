using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LockCombo : MonoBehaviour, IScrollable
{

    public EndLock lockItem;

    public int currentNum = 1;
    public int minNum = 1;
    public int maxNum = 26;

    public TextMeshPro letterText;
    private readonly char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();


    void UpdateLetterDisplay()
    {
        // int index = currentNum - 1; // 1..26 becomes 0..25
        // letterText.text = alphabet[index].ToString();
        if (letterText == null)
            return;

        letterText.text = alphabet[currentNum - 1].ToString();

    }

    void Start()
    {
        if (letterText == null)
            letterText = GetComponentInChildren<TextMeshPro>();

        UpdateLetterDisplay();
    }
    public void WasScrolled(float frameScrollValue)
    {
        //Increment current num based on scroll value
        currentNum -= Mathf.RoundToInt(frameScrollValue);
        //if input was positive
        
        //clamp based on min and max
        //if lower than min num
        if (currentNum < minNum) 
        { 
            currentNum = maxNum;
        }
        //if higher than max num
        if (currentNum > maxNum) 
        { 
            currentNum = minNum;
        }
        Debug.Log($"<color=yellow>lock number = {currentNum}</color>");

        UpdateLetterDisplay();
        //change sprite of safe
        if (lockItem != null)
            lockItem.checkStatus();
    }
}



