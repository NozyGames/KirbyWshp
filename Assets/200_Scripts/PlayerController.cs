using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ParticleSystemJobs;

public class PlayerController : MonoBehaviour
{
    #region F/P
    public float speed = 1.8f;
    public int power;
    public int distray;
    public float jumpForce;
    public bool onGround;
    public float horizontalInput;
    [SerializeField]
    private GameObject[] Enemies;
    [SerializeField]
    private Sprite[] powerups;
    public PowerEffects pe;
    [HideInInspector]
    public SpriteRenderer sr;
    [HideInInspector]
    public bool powerAction;
    Rigidbody2D rb;
    [SerializeField]
    GameObject aspiration;
    [SerializeField]
    SpriteRenderer aspirationSr;
    [SerializeField]
    ParticleSystem[] woosh;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        distray = 1;
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        //aspiration = FindObjectOfType<GameObject>(name == "Aspiration");
        aspiration.transform.position = new Vector3(0, 0, 0);
        //aspirationSr = FindObjectOfType<SpriteRenderer>(name == "Aspiration");
        jumpForce = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        #region Controller
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime, 0);
		
        bool jumpInput = Input.GetButton("Jumping");
        if (jumpInput && onGround)
        {
            onGround = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if (power == 1) woosh[1].Play();
        }
        bool dropPower = Input.GetButtonUp("Drop");
        if (dropPower && power != 0)
        {
            sr.sprite = powerups[0];
            power = 0;
            jumpForce = 5f;
            this.gameObject.layer = 7;
            speed = 2f;
            Enemies[0].SetActive(true);
            Enemies[1].SetActive(true);
            Enemies[2].SetActive(true);
        }
        powerAction = Input.GetButton("PowerAction");
        if (powerAction && power == 0)
        {
            sr.sprite = powerups[4];
            aspiration.SetActive(true);
            OnAbsorption();
        }
        else
        {
            sr.sprite = powerups[0];
            aspiration.SetActive(false);
        }
        #endregion
        #region Distray Inversion
        if (horizontalInput >= 0)
        {
            aspiration.transform.localPosition = new Vector3(1, 0, 0);
            aspirationSr.flipY = false;
            distray = 1;
            sr.flipX = false;
        }
        else
        {
            aspiration.transform.localPosition = new Vector3(-1, 0, 0);
            aspirationSr.flipY = true;
            distray = -1;
            sr.flipX = true;
        }
        #endregion
        #region Power
        switch (power)
        {
            case 1:
                sr.sprite = powerups[1];
                pe.FirePower();
                break;
            case 2:
                sr.sprite = powerups[2];
                if (powerAction) pe.IcePower();
                break;
            case 3:
                sr.sprite = powerups[3];
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
                woosh[0].Play();
                break;
            case "Ice":
                power = 2;
                woosh[0].Play();
                break;
            case "Electric":
                power = 3;
                woosh[0].Play();
                break;
        }
        hit.collider.gameObject.SetActive(false);
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "EndBox")
        {
            int ha = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(ha+1);
        }
        if (collision.gameObject.CompareTag("Ground")) onGround = true;
    }
}