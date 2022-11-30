using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public PlayerController pc;
    public int speed;
    public float lastInput;
    public float selfdestroy = 3f;
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        lastInput = pc.horizontalInput;
    }
    private void Update()
    {
        transform.Translate(speed * lastInput * Time.deltaTime, 0, 0);
        selfdestroy -= Time.deltaTime;
        if (selfdestroy <= 0) Destroy(gameObject);
    }
}
