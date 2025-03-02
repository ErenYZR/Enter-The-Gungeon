using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    //Input ve Movement
    public float movementSpeed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector3 mousePos;
    public Camera cam;
    private Vector3 lookingDir;

    [Header("DodgeRoll")]
	[SerializeField] float originalDodgeRollSpeed = 5f;
    [SerializeField] float currentDodgeRollSpeed;
    [SerializeField] float dodgeRollTime = 2f;
    [SerializeField] float dodgeRollCooldown = 0.2f;
    private bool canDodgeRoll;

    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private Health playerHealth;

	[SerializeField] public State state;
    public enum State
    {
        Normal,
        DodgeRoll,
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        state = State.Normal;
        canDodgeRoll = true;
        playerHealth = GetComponent<Health>();
        playerHealth.OnTakeDamage += PlayerDamageActions;
        
    }

	private void Awake()
	{
		playerController = new PlayerController();
	}

	void Update()
    {
        switch (state)
        {
            case State.Normal:			
				GetInput(); //Input alma kýsmý				
				GetMouseInput();//mouse'un kameradaki konumunu alýp ona bakmasýný saðlayan kod
                CheckDodgeRoll();
                break;
            case State.DodgeRoll:
                DodgeRoll();
                break;
		}
    }

	private void FixedUpdate()
	{
        //hareket kodu
		rb.velocity = moveInput.normalized * movementSpeed;
	}

    private void GetInput()//Input alma kýsmý
	{
        moveInput = playerController.Player.Move.ReadValue<Vector2>();
	}

    private void GetMouseInput()//mouse'un kameradaki konumunu alýp ona bakmasýný saðlayan kod
	{
		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = 0f;
		lookingDir = (mousePos - rb.transform.position).normalized;
		float angle = Mathf.Atan2(lookingDir.y, lookingDir.x) * Mathf.Rad2Deg - 90f;
		rb.transform.rotation = Quaternion.Euler(0f, 0f, angle);
	}

    private void CheckDodgeRoll()
    {
        if (Input.GetMouseButtonDown(1) && moveInput != new Vector2(0, 0) && canDodgeRoll)
        {
            state = State.DodgeRoll;
            currentDodgeRollSpeed = originalDodgeRollSpeed;
        }
    }

    private IEnumerator DodgeRollCoroutine()
    {
		spriteRenderer.color = Color.white;
		canDodgeRoll = false;
        yield return new WaitForSeconds(dodgeRollCooldown);
        canDodgeRoll = true;
    }
    private void DodgeRoll()
    {
		spriteRenderer.color = Color.black;
		rb.position += moveInput.normalized * currentDodgeRollSpeed * Time.deltaTime;
		currentDodgeRollSpeed -= (originalDodgeRollSpeed) / dodgeRollTime * Time.deltaTime;
        if(currentDodgeRollSpeed < 0.5f)
        {
            state = State.Normal;
            StartCoroutine(DodgeRollCoroutine());
        }
    }

	private void OnEnable()
	{
        playerController?.Enable();
	}

	private void OnDisable()
	{
        playerController?.Disable();
	}

    private void PlayerDamageActions(int damage)
    {
        StartCoroutine(TakingDamage());
    }

    private IEnumerator TakingDamage()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
