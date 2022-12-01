using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool isOpen;
    Transform doorPos;
    Vector2 startpos;
    private void Start()
    {
        doorPos = this.gameObject.GetComponent<Transform>();
        startpos = this.gameObject.GetComponent<Transform>().position;
    }
    private void Update()
    {
        if (isOpen) doorPos.position = new Vector2(700, 700);
        if (!isOpen) doorPos.position = startpos;
    }
}
