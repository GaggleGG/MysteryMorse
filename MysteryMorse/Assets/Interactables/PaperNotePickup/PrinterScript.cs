using UnityEngine;

public class PrinterScript : MonoBehaviour
{
    CreateTicket ticketBase = new CreateTicket();
    public Transform printPostion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        printTicket();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void printTicket()
    {
        ticketBase.createTicket(1);
        Instantiate(ticketBase.notePrefab,printPostion.position, Quaternion.identity);
        
    }

    
}
