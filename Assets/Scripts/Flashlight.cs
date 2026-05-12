using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject flash;
    public bool isTurn = false;
    public int battery = 100;
    public float timer;
    public int batteryUsage = 1;
    // Start is called before the first frame update
    void Start()
    {
        flash.GetComponent<Light>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(battery);
        if (Input.GetMouseButtonDown(0) && !isTurn)
        {
            flash.GetComponent<Light>().enabled = true;
            isTurn = true;
            
        }
        else if (Input.GetMouseButtonDown(0) && isTurn)
        {
            flash.GetComponent<Light>().enabled = false;
            isTurn = false;
        }
        if (isTurn)
        {

            timer += Time.deltaTime;
            if (timer >= 3)
            {
                timer = 0;
                battery -= batteryUsage; 
            }
        }
    }
}