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
    [SerializeField]
    SpriteRenderer IceBlock;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        IceBlock.color = new Color(0, 255, 12, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Ice Bullet(Clone)" && isWaddle)
        {
            isFrozen = true;
            if (isFrozen) IceBlock.color = new Color(0, 255, 12, 100);
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.gameObject.name == "Player" && isFrozen)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
