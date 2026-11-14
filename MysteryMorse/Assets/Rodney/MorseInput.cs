using UnityEngine;

public class MorseInput : MonoBehaviour, IScrollable
{
    [Header("Input Switch Bone")]
    [SerializeField]
    private GameObject SwitchBone;

    public void AnimateSwitchBone(float frameScrollValue)
    {
        if (frameScrollValue > 0)
        {
            SwitchBone.transform.localRotation = Quaternion.Euler(0, 0, 45);
        }
        else if (frameScrollValue < 0)
        {
            SwitchBone.transform.localRotation = Quaternion.Euler(0, 0, -45);
        }
        else
        {
            SwitchBone.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    
    public char ReturnDotOrDash(float frameScrollValue)
    {
        if (frameScrollValue > 0)
        {
            return '.';
        }
        else if (frameScrollValue < 0)
        {
            return '-';
        }
        else
        {
            return ' ';
        }
    }










    //takes up scroll input from the player and sends it to the morse code processing unit.



    //takes down the scroll input from the player and sends it to the morse code processing unit.



    //Animate the bone of input switch






    public void WasScrolled(float frameScrollValue)
    {
        //if you want to double chekc nly output when not 0
        
        if (frameScrollValue > 0)
        {
            Debug.Log("<color=green>Positive Scroll Value Recieved: " + frameScrollValue + " Dot</color>");
        }
        else if (frameScrollValue < 0)
        {
            Debug.Log("<color=red>Negative Scroll Value Recieved: " + frameScrollValue + " Dash</color>");
            
        }
        else
        {
            Debug.Log("<color=blue>Zero Scroll Value Recieved: " + frameScrollValue + " Space</color>");
           
        }



    }

}
