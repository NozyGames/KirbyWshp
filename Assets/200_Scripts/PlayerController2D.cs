using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController2D : MonoBehaviour
{
    // Vitesse de déplacement du personnage
    public float speed = 5.0f;

    // Force de saut du personnage
    public float jumpForce = 10.0f;

    // Composant Rigidbody2D du personnage
    private Rigidbody2D rb;

    // Indicateur pour savoir si le personnage est en contact avec un mur
    [SerializeField]
    private bool isTouchingWall = false;

    [SerializeField]
    private bool onGround;

    void Start()
    {
        // Récupérer le composant Rigidbody2D du personnage
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Récupérer les entrées de mouvement du joueur
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Appliquer les entrées de mouvement au personnage en utilisant la physique intégrée de Unity
        Vector2 movement = new Vector2(moveHorizontal, 0);
        rb.velocity = movement * speed;


        bool jumpInput = Input.GetButton("Jump");
        if (jumpInput && onGround)
        {
            onGround = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.y, 0);
        }

        // Si le personnage est en contact avec un mur et qu'il maintient la touche de saut, arrêter l'application de la force de saut
        if (isTouchingWall && jumpInput)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Si le personnage est en contact avec un mur, mettre à jour l'indicateur isTouchingWall
        if (collision.gameObject.tag == "Walls")
        {
            isTouchingWall = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "EndBox")
        {
            SceneManager.LoadScene(1);
        }
        if (collision.collider.gameObject.tag == "Ground") onGround = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Si le personnage n'est plus en contact avec un mur, mettre à jour l'indicateur isTouchingWall
        if (collision.gameObject.tag == "Walls")
        {
            isTouchingWall = false;
        }
    }
}