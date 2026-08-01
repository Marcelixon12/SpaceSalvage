using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour
{
    public int batteryInMagazine = 100;
    
    public int chargeSpeed = 5;
    public float holdTime = 3;
    public float timer = 0;

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
        if (other.gameObject.CompareTag("Player"))
        {
            Flashlight flashlight = other.gameObject.GetComponentInChildren<Flashlight>();
            
                                                          
            if (flashlight != null && batteryInMagazine > 0 && flashlight.battery < 100)
            {
                
                if (Input.GetKey(KeyCode.E))
                {
                    timer += Time.deltaTime;
                   
                    if (timer >= holdTime)
                    {
                        int neededEnergy = 100 - flashlight.battery;
                        timer = 0;
                        if (batteryInMagazine >= neededEnergy)
                        {
                            flashlight.battery += neededEnergy;  
                            batteryInMagazine -= neededEnergy;   
                        }
                        else
                        {
                            flashlight.battery += batteryInMagazine; 
                            batteryInMagazine = 0;
                        }
                    }
                }
                else if(Input.GetKeyUp(KeyCode.E))
                {
                    timer = 0f;
                }
            }

            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer = 0f;
           
        }
    }
}
