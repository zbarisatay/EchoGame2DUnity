using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float deadDistance = -15f;
    public Animator animator;

    private Rigidbody2D rb;
    private float moveInput;
    private bool hasJumped = false;
    private bool _isGameOver = false;

    private MainMenuController _mainMenuController;

    private const string CHARACTER_X = "character-x";
    private const string CHARACTER_Y = "character-y";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _mainMenuController = FindObjectOfType<MainMenuController>();
    }

    void OnEnable()
    {
        if (PlayerPrefs.HasKey(CHARACTER_X))
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat(CHARACTER_X), PlayerPrefs.GetFloat(CHARACTER_Y));
            Debug.Log("Has Key Character X");
        }
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (Input.GetButtonDown("Jump") && !hasJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            hasJumped = true;
            animator.SetTrigger("Jump");  // Trigger tetikle
        }
    }

    private void OnDisable()
    {
        // Save
        SavePosition();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            hasJumped = false;
            // Burada artık bool sıfırlamaya gerek yok
            // animasyonun bitişi animator tarafından kontrol edilecek
        }

        /*
        if (collision.gameObject.CompareTag("Obstacle")) 
        {
            MainMenuController.Instance.ShowGameOver();
        }
        */
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(moveInput));

        if (transform.position.y < deadDistance)
        {
            _isGameOver = true;
            DeletePositionData();
            _mainMenuController.ShowGameOver();
        }
    }

    public void SavePosition()
    {
        if (_isGameOver)
        {
            return;
        }
        PlayerPrefs.SetFloat(CHARACTER_X, transform.position.x);
        PlayerPrefs.SetFloat(CHARACTER_Y, transform.position.y);
    }

    public void DeletePositionData()
    {
        if (!PlayerPrefs.HasKey(CHARACTER_X) && !PlayerPrefs.HasKey(CHARACTER_Y))
        {
            return;
        }
        Debug.Log("Reset Data");
        PlayerPrefs.DeleteKey(CHARACTER_X);
        PlayerPrefs.DeleteKey(CHARACTER_Y);
    }
}
