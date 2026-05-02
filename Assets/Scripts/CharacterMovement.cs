using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 4f;
    public float shiftSpeed = 8f;
    public float currentSpeed;
    [SerializeField] float lowJumpForce = 6f;
    public float highJumpForce = 10f;
    public float currentJumpForce;
    Vector3 direction;
    public bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = moveSpeed;
        currentJumpForce = highJumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, currentJumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.fixedDeltaTime);

    }
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        
    }
}
