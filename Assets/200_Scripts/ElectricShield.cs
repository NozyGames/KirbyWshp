
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ElectricShield : MonoBehaviour
{
    PlayerController pc;
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (pc.powerAction && this.gameObject.transform.localScale.x <= 0.7f)
        {
            this.gameObject.transform.localScale += new Vector3(0.15f * Time.deltaTime, 0.15f * Time.deltaTime, 0);
            pc.speed = 0f;
        }
        else if (!pc.powerAction)
        {
            Destroy(this.gameObject);
            pc.speed = 2;
        }
    }
}
