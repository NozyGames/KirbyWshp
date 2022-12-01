using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool isOpen;
    Transform isNotHereAnymore;
    Vector2 startpos;
    private void Start()
    {
        isNotHereAnymore = this.gameObject.GetComponent<Transform>();
        startpos = this.gameObject.GetComponent<Transform>().position;
    }
    private void Update()
    {
        if (isOpen) isNotHereAnymore.position = new Vector2(700, 700);
        if (!isOpen) isNotHereAnymore.position = startpos;
    }
}
