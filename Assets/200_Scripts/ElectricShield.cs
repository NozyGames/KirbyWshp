using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricShield : MonoBehaviour
{
    public PlayerController pc;
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (pc.powerAction) this.gameObject.transform.localScale += new Vector3(0.1f * Time.deltaTime, 0.1f * Time.deltaTime, 0);
        //transform.Translate(pc.GetComponent<Transform>().position);
    }
}
