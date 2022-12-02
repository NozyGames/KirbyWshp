using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.8f;
	float walkAcceleration;
	float groundDeceleration;
	BoxCollider2D boxCollider;
	Vector2 velocity;
    public int power;
    public int distray;
    public float jumpForce;
    public bool onGround;
    [SerializeField]
    private GameObject[] Enemies;
    public float horizontalInput;
    [HideInInspector]
    public PowerEffects pe;
    [HideInInspector]
    public SpriteRenderer sr;
    [HideInInspector]
    public bool powerAction;
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
        #region Controller
        horizontalInput = Input.GetAxisRaw("Horizontal");
		if (horizontalInput != 0)
		{
			velocity.x = Mathf.MoveTowards(velocity.x, speed * horizontalInput, walkAcceleration * Time.deltaTime);
		}
		else
		{
			velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
		}
		
		Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
		foreach (Collider2D hit in hits)
{
		if (hit == boxCollider)
		continue;

		ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

		if (colliderDistance.isOverlapped)
		{
			transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
		}
}
		
        bool jumpInput = Input.GetButton("Jump");
        if (jumpInput && onGround)
        {
            onGround = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        bool dropPower = Input.GetButtonUp("Drop");
        if (dropPower && power != 0)
        {
            sr.color = new Color(255, 255, 255);
            power = 0;
            jumpForce = 5f;
            this.gameObject.layer = 7;
            speed = 1.8f;
            Enemies[0].SetActive(true);
            Enemies[1].SetActive(true);
            Enemies[2].SetActive(true);
        }
        powerAction = Input.GetButton("PowerAction");
        if (powerAction && power == 0) OnAbsorption();
        #endregion
        #region Distray Inversion
        if (horizontalInput >= 0) distray = 1;
        else distray = -1;
        #endregion
        #region Power
        switch (power)
        {
            case 1:
                sr.color = new Color(255, 0, 0);
                pe.FirePower();
                break;
            case 2:
                sr.color = new Color(0, 255, 255);
                if (powerAction) pe.IcePower();
                break;
            case 3:
                sr.color = new Color(255, 228, 0);
                if (powerAction) pe.ElectricPower();
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
        if (!hit || hit.collider.gameObject.name == "Neutral" || hit.collider.gameObject.name == "Batteire 1" || hit.collider.gameObject.name == "Batterie 2") return;
        switch (hit.collider.gameObject.name)
        {
            case "Fire":
                power = 1;
                break;
            case "Ice":
                power = 2;
                break;
            case "Electric":
                power = 3;
                break;
        }
        hit.collider.gameObject.SetActive(false);
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "EndBox")
        {
            SceneManager.LoadScene(1);
        }
        onGround = true;
    }
}