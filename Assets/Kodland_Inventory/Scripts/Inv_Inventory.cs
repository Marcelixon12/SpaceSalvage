using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Inv_Inventory : MonoBehaviour
{    
    [SerializeField] List<Button> buttons = new List<Button>();  
    [SerializeField] List<GameObject> resourceItems = new List<GameObject>();
    [SerializeField] GameObject buttonsPath;
    [SerializeField] List<string> inventoryItems = new List<string>();
    public GameObject itemInArm;
    [SerializeField] Transform itemPoint;
    [SerializeField] Transform[] itemPositions;
    [SerializeField] TMP_Text warning;
    [SerializeField] List<GameObject> playerItems = new List<GameObject>();
    GameObject itemPosition;
    
    [SerializeField] List<Sprite> inventoryItemSprites = new List<Sprite>();
    [SerializeField] List<Sprite> defaultButtonSprites = new List<Sprite>();
    public CharacterMovement chara;



    private void Start()
    {
        GameObject[] objArr = Resources.LoadAll<GameObject>("Space");
        resourceItems.AddRange(objArr);
        foreach (Transform child in buttonsPath.transform)
        {
            Button btn = child.GetComponent<Button>();
            buttons.Add(btn);
            defaultButtonSprites.Add(btn.GetComponent<Image>().sprite);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseItem(3);
        }
    }
    public void AddItem(Sprite img, string itemName, GameObject obj)
    {
        if (inventoryItems.Count >= buttons.Count)
        {
            warning.text = "Full Inventory!";
            Invoke("WarningUpdate", 1f);
            return;
        }
        
        inventoryItems.Add(itemName);
        if (itemName == "GeneratorCore")
        {
            chara.cores += 1;
        }

        // Przypisujemy grafikê do aktualnego slotu
        var buttonImage = buttons[inventoryItems.Count - 1].GetComponent<Image>();
        buttonImage.sprite = img;
        buttonImage.color = Color.white; // Upewniamy siê, ¿e slot jest widoczny

        Destroy(obj);
    }

    void WarningUpdate()
    {
        warning.text = "";
    }
    public void UseItem(int itemPos)
    {
        // Zabezpieczenie: czyœcimy listê playerItems z obiektów, które zosta³y zniszczone (s¹ nullami)
        playerItems.RemoveAll(x => x == null);

        if (inventoryItems.Count <= itemPos) return;
        string item = inventoryItems[itemPos];
        GetItemFromInventory(item);
    }

    public void GetItemFromInventory(string itemName)
    {
        Debug.Log("U¿ywam przedmiotu: " + itemName);

        
        if (itemInArm == null)
        {
            itemInArm = null;
        }

        var resourceItem = resourceItems.Find(x => x.name == itemName);
        if (resourceItem == null) return;

        
        var putFind = playerItems.Find(x => x != null && x.name == itemName);

        if (putFind == null)
        {
            
            if (itemInArm != null)
            {
                itemInArm.SetActive(false);
            }

            var pos = resourceItem.GetComponent<Inv_ItemPosition>().positon;
            if (pos == Inv_ItemPosition.ItemPos.Head)
            {
                itemPoint.position = itemPositions[0].position;
                itemPosition = itemPositions[0].gameObject;
            }
            else if (pos == Inv_ItemPosition.ItemPos.Spine)
            {
                itemPoint.position = itemPositions[1].position;
                itemPosition = itemPositions[1].gameObject;
            }
            else
            {
                itemPoint.position = itemPositions[2].position;
                itemPosition = itemPositions[2].gameObject;
            }

            var newItem = Instantiate(resourceItem, itemPoint);
            newItem.transform.parent = itemPosition.transform;
            newItem.name = itemName;
            playerItems.Add(newItem);
            itemInArm = newItem;
        }
        else
        {
            if (putFind == itemInArm)
            {
                putFind.SetActive(!putFind.activeSelf);
            }
            else
            {
                
                if (itemInArm != null)
                {
                    itemInArm.SetActive(false);
                }

                putFind.SetActive(true);
                itemInArm = putFind;
            }
        }
    }

    public bool HasItem(string itemName)
    {
        return inventoryItems.Contains(itemName);
    }


    public void RemoveItem(string itemName)
    {
        int itemIndex = inventoryItems.IndexOf(itemName);
        if (itemIndex == -1) return; 

        
        if (itemInArm != null && (itemInArm.name == itemName || itemInArm.name.StartsWith(itemName)))
        {
            Destroy(itemInArm);
            itemInArm = null;
        }

        
        if (itemPositions != null)
        {
            foreach (Transform pos in itemPositions)
            {
                if (pos == null) continue;
                for (int i = pos.childCount - 1; i >= 0; i--)
                {
                    Transform child = pos.GetChild(i);
                    if (child.name == itemName || child.name.StartsWith(itemName))
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
        }

        
        inventoryItems.RemoveAt(itemIndex);

        
        RedrawInventoryUI(itemIndex);

        Debug.Log("Usuniêto z ekwipunku: " + itemName);
    }

    
    private void RedrawInventoryUI(int removedIndex)
    {
       
        for (int i = removedIndex; i < buttons.Count; i++)
        {
            var currentButtonImage = buttons[i].GetComponent<Image>();

            
            if (i + 1 < buttons.Count && i < inventoryItems.Count)
            {
                var nextButtonImage = buttons[i + 1].GetComponent<Image>();
                currentButtonImage.sprite = nextButtonImage.sprite;
                currentButtonImage.color = Color.white;
            }
            else
            {
                
                if (i < defaultButtonSprites.Count)
                {
                    currentButtonImage.sprite = defaultButtonSprites[i];
                    currentButtonImage.color = Color.white;
                }
            }
        }
    }

}
