using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrozen : MonoBehaviour
{
    [SerializeField]
    bool isFrozen;
    [SerializeField]
    bool isWaddle = true;
    Rigidbody2D rb;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Ice Bullet(Clone)" || isWaddle)
        {
            isFrozen = true;
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.gameObject.name == "Player" && isFrozen)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
