using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dron"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Debug.Log("Dron colliduje ze œcian¹");
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                other.transform.Rotate(0f, 180f, 0f);

                rb.MovePosition(other.transform.position);
                rb.MoveRotation(other.transform.rotation);
            }
        }
        
    }
}