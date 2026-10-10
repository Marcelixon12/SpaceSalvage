using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
   
    public float speed = 4f;
    AudioSource a;
    public AudioClip clip;
    public int cardLevel;
    public CharacterMovement card;
    [SerializeField] private string requiredCardName = "MicroSD"; 
    public GameObject canvas;
    private bool isDoorOpen = false;
    Animator anim;
    Vector3 upPos;
    Vector3 downPos;
    bool movingUp = false;
    bool movingDown = false;
    // Start is called before the first frame update
    void Start()
    {
       
        a = GetComponent<AudioSource>();
        
        anim = GetComponent<Animator>();
        upPos = new Vector3(transform.position.x, 0, transform.position.z);
        downPos = new Vector3(transform.position.x, transform.position.y - 4f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, downPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, downPos) < 0.01f)
            {
                movingDown = false;
                isDoorOpen = true;
            }
        }
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, upPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, upPos) < 0.01f)
            {
                movingUp = false;
                isDoorOpen = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            Inv_Inventory inventory = canvas.GetComponentInChildren<Inv_Inventory>();

            if (inventory != null)
            {
                
                if (inventory.itemInArm != null)
                {
                    
                    if (inventory.itemInArm.name == requiredCardName && card.Level >= cardLevel && !isDoorOpen)
                    {

                        movingDown = true;
                        a.PlayOneShot(clip);
                        
                        
                    }
                    
                }
                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isDoorOpen)
        {
            movingUp = true;
            a.PlayOneShot(clip);
            
            
        }
    }

}
