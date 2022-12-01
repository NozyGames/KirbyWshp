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
        if (pc.powerAction && this.gameObject.transform.localScale.x <= 1.8f)
        {
            this.gameObject.transform.localScale += new Vector3(0.2f * Time.deltaTime, 0.2f * Time.deltaTime, 0);
            pc.speed = 0f;
        }
        else if (!pc.powerAction)
        {
            Destroy(this.gameObject);
            pc.speed = 1.8f;
        }
    }
}
