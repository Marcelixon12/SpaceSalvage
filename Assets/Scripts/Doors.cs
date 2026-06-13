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
    [SerializeField] private string requiredCardName = "MicroSD"; // Nazwa karty w ekwipunku i Resources
    public GameObject canvas;
    private bool isDoorOpen = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       
        a = GetComponent<AudioSource>();
        
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            // 2. Szukamy skryptu ekwipunku na graczu
            Inv_Inventory inventory = canvas.GetComponentInChildren<Inv_Inventory>();

            if (inventory != null)
            {
                // 3. Sprawdzamy, czy gracz w ogóle trzyma coœ w rêce
                if (inventory.itemInArm != null)
                {
                    // 4. KLUCZOWY MOMENT: Sprawdzamy, czy nazwa trzymanego przedmiotu to nasza karta
                    if (inventory.itemInArm.name == requiredCardName && card.Level >= cardLevel)
                    {

                        anim.SetBool("Entry", true);
                        a.PlayOneShot(clip);
                        
                        isDoorOpen = true;
                    }
                    else
                    {
                        Debug.Log("Trzymasz coœ innego! Musisz wyci¹gn¹æ kartê: " + requiredCardName);
                    }
                }
                else
                {
                    Debug.Log("Twoje rêce s¹ puste. Wyci¹gnij kartê z ekwipunku!");
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isDoorOpen)
        {
            anim.SetBool("Entry", false);
            a.PlayOneShot(clip);
            isDoorOpen = false;
            
        }
    }

}
