using Unity.VisualScripting;
using UnityEngine;


public class CreateTicket
{
    public GameObject notePrefab;
    
    public void createTicket(int SolutionID)
    {
        notePrefab = Resources.Load("NotePickup") as GameObject;
        notePrefab.GetComponent<NotePickup>().solutionID = SolutionID;
    }
}
