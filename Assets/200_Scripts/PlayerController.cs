using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.5f;
    public int power;
    //private BoxCollider2D AbsorbCollider;
    public int distray;
    // Start is called before the first frame update
    void Start()
    {
        //AbsorbCollider = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 6;
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(speed * horizontalInput *Time.deltaTime, 0, 0);
        bool absorption = Input.GetButtonDown("Absorb");
        //absorption) AbsorbCollider.enabled = true;
        // AbsorbCollider.enabled = false;
        if (absorption)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * distray, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), distray, layerMask);
            switch (hit.collider.gameObject.name)
            {
                case "Fire":
                    power = 1;
                    break;

            }
        }
    }
}
