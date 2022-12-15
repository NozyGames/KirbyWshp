using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Doors exitDoor;
    public Doors[] notExit;
    private void Start()
    {
        //exitDoor = FindObjectOfType<Doors>(name == "Exit");
        //notExit = FindObjectsOfType<Doors>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Neutral")
        {
            exitDoor.isOpen = true;
            notExit[0].isOpen = false;
            notExit[1].isOpen = false;
        }

    }
}