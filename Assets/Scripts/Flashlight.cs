using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject flash;
    public bool isTurn = false;
    public int battery = 100;
    public int maxBattery = 100;
    public float timer;
    public int batteryUsage = 1;
    public TMP_Text batteryText;
    // Start is called before the first frame update
    void Start()
    {
        flash.GetComponent<Light>().enabled = false;
        batteryText = GameObject.FindGameObjectWithTag("Battery").GetComponent<TMP_Text>();
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
}