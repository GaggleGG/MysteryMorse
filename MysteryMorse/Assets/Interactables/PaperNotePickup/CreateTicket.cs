using Unity.VisualScripting;
using UnityEngine;


public class Ticket
{
    public GameObject notePrefab;

    
    public void CreateTicket(int SolutionID)
    {
        notePrefab = Resources.Load("NotePickup") as GameObject;
        notePrefab.GetComponent<NotePickup>().solutionID = SolutionID;
    }
}
