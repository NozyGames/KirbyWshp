using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEffects : MonoBehaviour
{
    public PlayerController pc;
    public GameObject IceBullet;
    public GameObject ElectricShield;
    public void FirePower()
    {
        pc.jumpForce = 9f;
    }

    public void IcePower()
    {
        bool isThereBullet = FindObjectOfType<IceBullet>();
        if (!isThereBullet) Instantiate(IceBullet, pc.GetComponent<Transform>().position, Quaternion.Euler(0, 0, 0));
        pc.gameObject.layer = 9;
    }

    public void ElectricPower()
    {
        bool isThereShield = FindObjectOfType<ElectricShield>();
        if(!isThereShield) Instantiate(ElectricShield, pc.GetComponent<Transform>().position, Quaternion.Euler(0, 0, 0));
    }
}
