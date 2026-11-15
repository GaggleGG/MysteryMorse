using UnityEngine;

public class PrinterScript : MonoBehaviour
{

    public Terminal terminal;
    
    public Transform printPostion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //spawn ticket behind priinter and animate before to extent and remove at threshold
    // Check the state of the Terminal and if it is Create Ticket, print the ticket and set the state back to Waiting

    public void ValidityCheck()
    {
        if(terminal.state != Terminal.TerminalState.WaitingForTicket)
        {
            Debug.Log("IM PRINTING IT");
            printTicket(terminal.ticket);
        }
    }

    


    public void printTicket(Ticket ticket)
    {

        Instantiate(ticket.notePrefab,printPostion.position, Quaternion.identity);
        
    }

    
}
