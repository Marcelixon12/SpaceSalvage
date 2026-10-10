using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject flash;
    public bool isTurn = false;
    [NonSerialized] public int battery = 100;
    public int maxBattery = 100;
    public float timer;
    public int batteryUsage = 1;
    public TMP_Text batteryText;
    
    [NonSerialized] public bool isLoaded = false;
    void Start()
    {
        if (!isLoaded)
            battery = 100;


        flash.GetComponent<Light>().enabled = false;
        batteryText = GameObject.FindGameObjectWithTag("Battery").GetComponent<TMP_Text>();
        BatteryTextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(battery);
        
        
        if (battery > 0)
        {
            FlashlightUse();
        }
        else if (battery <= 0)
        {
            battery = 0;

        }
        if (battery >= maxBattery)
        {
            battery = maxBattery;
        }
        BatteryTextUpdate();
        
    }
    //public void OnTriggerStay(Collider other)
    //{
       // if (other.gameObject.CompareTag("Charger") && Input.GetKey(KeyCode.E) && other.gameObject.GetComponent<Charger>().batteryInMagazine > 0)
        //{
            //battery += other.gameObject.GetComponent<Charger>().chargeSpeed;
           // other.gameObject.GetComponent<Charger>().batteryInMagazine -= other.gameObject.GetComponent<Charger>().chargeSpeed;
        //}
    //}
    public void BatteryTextUpdate()
    {
        batteryText.text = "Battery: " + battery.ToString() + "%";
    }
    public void FlashlightUse()
    {
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
            if (timer >= 5)
            {
                timer = 0;
                battery -= batteryUsage;
            }
        }
    }
    public void SetBattery(int value)
    {
        battery = value;
        BatteryTextUpdate();
    }
}