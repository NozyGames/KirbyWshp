using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    SpriteRenderer sr;
    public bool isOn;
    public Doors door;
    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "ElectricShield(Clone)")
        {
            sr.color = new Color(255, 228, 0);
            door.isOpen = true;
        }
    }
}