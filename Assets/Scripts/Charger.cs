using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    public int batteryInMagazine = 100;
    
    public int chargeSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && batteryInMagazine > 0 && other.gameObject.GetComponent<Flashlight>().battery < 100)
        {
            Flashlight flashlight = other.GetComponentInChildren<Flashlight>();

            if (flashlight != null)
            {
                flashlight.battery += chargeSpeed;
            }

            batteryInMagazine -= chargeSpeed;
        }
    }
}
