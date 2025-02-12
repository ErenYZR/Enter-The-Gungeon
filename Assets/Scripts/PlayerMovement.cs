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
    




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input alma kýsmý
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //mouse'un kameradaki konumunu alýp ona bakmasýný saðlayan kod
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        lookingDir = (mousePos - rb.transform.position).normalized;
        float angle = Mathf.Atan2(lookingDir.y,lookingDir.x) * Mathf.Rad2Deg-90f;
        rb.transform.rotation = Quaternion.Euler(0f,0f,angle);
    }

	private void FixedUpdate()
	{
        //hareket kodu
		rb.velocity = moveInput.normalized * movementSpeed;
	}
}
