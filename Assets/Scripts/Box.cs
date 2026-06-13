using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject Code;
    public string trueCode;
    public UI u;
    Animator anim;
    public GameObject upgrader;
    public GameObject upgraderLight;

    
    private bool isPlayerHere = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (isPlayerHere && u.code.Length == 4)
        {
            Check();
        }

        
        if (isPlayerHere)
        {
            Debug.Log("Ta skrzynia oczekuje: " + trueCode + " | Wpisano: " + u.code);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerHere = true; 
            u.code = ""; 
            Code.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerHere = false; 
            Code.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            u.code = ""; 
        }
    }

    public void Check()
    {
        if (u.code == trueCode)
        {
            Debug.Log("Skrzynia otwarta");
            isPlayerHere = false; 
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Code.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            anim.SetBool("Open", true);
            upgrader.SetActive(true);
            upgraderLight.GetComponent<Light>().enabled = true;
            u.code = "";
        }
        else
        {
            Debug.Log("Nieprawid³owy kod");
            u.code = "";
        }
    }
}