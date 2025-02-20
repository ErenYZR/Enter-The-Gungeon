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

	[SerializeField] private State state;
    private enum State
    {
        Normal,
        DodgeRoll,
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.Normal;
    }

    // Update is called once per frame
    void Update()
    {

        print(moveInput.magnitude);
        switch (state)
        {
            case State.Normal:			
				GetInput(); //Input alma kýsmý				
				GetMouseInput();//mouse'un kameradaki konumunu alýp ona bakmasýný saðlayan kod
                CheckDodgeRoll();
                Stop();
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
		moveInput.x = Input.GetAxisRaw("Horizontal");
		moveInput.y = Input.GetAxisRaw("Vertical");
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
        if (Input.GetMouseButtonDown(1) && moveInput != new Vector2(0, 0))
        {
            state = State.DodgeRoll;
            currentDodgeRollSpeed = originalDodgeRollSpeed;
        }
    }

    private void DodgeRoll()
    {
		rb.position += moveInput.normalized * currentDodgeRollSpeed * Time.deltaTime;
		currentDodgeRollSpeed -= (originalDodgeRollSpeed) / dodgeRollTime * Time.deltaTime;
        if(currentDodgeRollSpeed < 0.5f)
        {
            state = State.Normal;
        }
    }

    private void Stop()
    {
        if(moveInput.magnitude == 0)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
