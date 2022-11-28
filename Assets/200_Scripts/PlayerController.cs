using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.5f;
    public int power;
    public int distray;
    public float jumpForce;
    public bool onGround;
    [HideInInspector]
    public PowerEffects pe;
    public SpriteRenderer sr;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        distray = 1;
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        jumpForce = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        #region controller
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(speed * horizontalInput * Time.deltaTime, 0, 0);
        bool jumpInput = Input.GetButton("Jump");
        if (jumpInput && onGround)
        {
            onGround = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        bool dropPower = Input.GetButtonUp("Drop");
        if (dropPower && power >= 0)
        {
            sr.color = new Color(255, 255, 255);
            power = 0;
            jumpForce = 5f;
        }
        bool absorption = Input.GetButtonDown("Absorb");
        if (absorption && power == 0) OnAbsorption();
        #endregion
        #region Distray Inversion
        if (horizontalInput >= 0)
        {
            distray = 1;
        }
        else distray = -1;
        #endregion
        #region power
        switch (power)
        {
            case 1:
                sr.color = new Color(255, 0, 0);
                pe.FirePower();
                break;
            case 2:
                sr.color = new Color(0, 255, 255);
                pe.IcePower();
                break;
            case 3:
                sr.color = new Color(255, 228, 0);
                pe.ElectricPower();
                break;
        }
        #endregion
    }

    #region OnAbsorption
    public void OnAbsorption()
    {
        int layerMask = 1 << 6;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * distray, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), distray, layerMask);
        switch (hit.collider.gameObject.name)
        {
            case "Fire":
                Destroy(hit.collider.gameObject);
                power = 1;
                break;
            case "Ice":
                Destroy(hit.collider.gameObject);
                power = 2;
                break;
            case "Electric":
                Destroy(hit.collider.gameObject);
                power = 3;
                break;
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D col)
    {
        onGround = true;
    }
}