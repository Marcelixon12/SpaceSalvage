using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 2f;
    public GameObject flash;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            
            Debug.Log("Paj¹k colliduje ze œcian¹");
            
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                gameObject.transform.Rotate(0f, 180f, 0f);

                rb.MovePosition(transform.position);
                rb.MoveRotation(transform.rotation);
           
        }

    }
}
