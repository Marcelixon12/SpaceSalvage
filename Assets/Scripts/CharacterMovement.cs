using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 4f;
    public float shiftSpeed = 8f;
    public float currentSpeed;
    [SerializeField] float JumpForce = 6f;
    
    Vector3 direction;
    public bool isGrounded = false;
    public GameObject flashl;
    // Start is called before the first frame update
    void Start()
    {
        TurnOffAllLights();
        rb = GetComponent<Rigidbody>();
        currentSpeed = moveSpeed;
        
        Physics.gravity = new Vector3(0, -1f, 0);
        flashl.GetComponent<Light>().enabled = true;
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
            rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
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
    public void TurnOffAllLights()
    {
        // Znajduje wszystkie œwiat³a (aktywne i nieaktywne) w scenie
        Light[] allLights = FindObjectsByType<Light>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Light l in allLights)
        {
            l.enabled = false;
        }

        Debug.Log("Wszystkie œwiat³a zosta³y wy³¹czone!");
    }
}
