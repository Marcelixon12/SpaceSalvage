using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 4f;
    public float shiftSpeed = 7f;
    public float currentSpeed;
    [SerializeField] float JumpForce = 6f;
    public int Level = 0;
    
    Vector3 direction;
    public bool isGrounded = false;
    public GameObject flashl;
    public GameObject cardLight;
    public GameObject helmetLight;
    public float oxygen = 100;
    public float oxygenUsage = 0;
    public float oxygenTimer = 0f;
    public bool isPut = false;
    public int cores = 0;
    public Inv_Inventory inv;
    public GameObject core;
    public GameObject[] coreSpawns;
    public TMP_Text oxygenText;
    public TMP_Text cardText;
    public GameObject Mask;
    public GameObject cardText2;
    public GameObject Helmet;
    public TMP_Text missionText;
    public string mission1 = "Put on Oxygen Mask: ";
    public string mission2 = "Find Secuirity Pass in your room";
    public string mission3 = "Find something to light your way";
    public string mission4 = "Upgrade the pass to get to the generator room";
    public string mission5 = "Go to the generator room and check what's wrong";
    public string mission6 = "Find 5 generator cores and insert them: ";
    public string actualMission;
    public float oxygenTimerUI = 5f;




    // Start is called before the first frame update
    void Start()
    {
        TurnOffAllLights();
        rb = GetComponent<Rigidbody>();
        currentSpeed = moveSpeed;
        
        Physics.gravity = new Vector3(0, -7f, 0);
        flashl.GetComponent<Light>().enabled = true;
        cardLight.GetComponent<Light>().enabled = true;
        helmetLight.GetComponent<Light>().enabled = true;
        oxygenUsage = 0.2f;
        oxygenTimerUI = 5f;
        actualMission = mission1 + Mathf.FloorToInt(oxygenTimerUI).ToString() + "s"; 
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            currentSpeed = shiftSpeed;
            oxygenUsage = 0.35f;
            
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && direction.x == 0 && direction.z == 0)
        {
            
            currentSpeed = moveSpeed;
            oxygenUsage = 0.05f;

        }
        else if (!Input.GetKey(KeyCode.LeftShift) && direction.x != 0 && direction.z != 0)
        {

            currentSpeed = moveSpeed;
            oxygenUsage = 0.1f;

        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            oxygen -= 0.15f;
            
        }
        if(isPut)
        {
            oxygenTimer += Time.deltaTime;
            if (oxygenTimer >= 1.5f)
            {
                oxygenTimer = 0f;
                oxygen -= oxygenUsage;
            }
            Mask.SetActive(true);
            Helmet.SetActive(false);
            cardText2.SetActive(true);
            
        }
        

            Debug.Log(oxygen);
        if (oxygen == 0)
        {
            Time.timeScale = 0f;
        }
        if (!isPut)
        {
            if (oxygenTimer >= 1f)
            {
                oxygenTimer = 0f;
                oxygen -= 20; 
            }
            Mask.SetActive(false);
            Helmet.SetActive(true);
            actualMission = mission1 + Mathf.FloorToInt(oxygenTimerUI).ToString() + "s";
        }
        if (oxygen >= 100)
        {
            oxygen = 100f;
        }
        if (oxygenTimerUI <= 0f)
        {
            oxygenTimerUI = 0f;
        }
        else
            oxygenTimerUI -= Time.deltaTime;
        TextsUpdate();
        if (actualMission == mission6)
        {
            actualMission = mission6 + "(" + cores.ToString() + "/5)";
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Helmet") && !isPut)
        {
            isPut = true;
            oxygen = 100f;
            other.gameObject.SetActive(false);
            Mask.SetActive(true);
            actualMission = mission2;
            

        }
        if (other.gameObject.CompareTag("Bottle"))
        {
            oxygen += 20f;

            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("DroneLight") && inv.HasItem("GeneratorCore"))
        {
            inv.RemoveItem("GeneratorCore");
            SpawnNewCore();
        }
        if (other.gameObject.CompareTag("Spider"))
        {
            oxygen -= 5;
        }
        if(other.gameObject.CompareTag("Card"))
        {
            Level = 1;
            cardText2.SetActive(true);
            actualMission = mission3;
        }
        if (other.gameObject.CompareTag("Flashlight"))
        {
            actualMission = mission4;
        }
        if (other.gameObject.CompareTag("Generator"))
        {
            actualMission = mission6;
        }
    }
    public void TurnOffAllLights()
    {
        // Znajduje wszystkie œwiat³a (aktywne i nieaktywne) w scenie
        Light[] allLights = FindObjectsByType<Light>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Light l in allLights)
        {
            l.enabled = false;
        }

       
    }
    public void SpawnNewCore()
    {
        int spawnPoint = Random.Range(0, coreSpawns.Length);

        Instantiate(core, new Vector3(coreSpawns[spawnPoint].transform.position.x + Random.Range(-1, 1), coreSpawns[spawnPoint].transform.position.y, coreSpawns[spawnPoint].transform.position.z + Random.Range(-1, 1)), Quaternion.identity);
    }
    public void TextsUpdate()
    {
        oxygenText.text = "O2: " + Mathf.FloorToInt(oxygen).ToString() + "%";
        cardText.text = "Card Level: " + Level.ToString();
        missionText.text = "Mission: " + actualMission;
    }
    
}
