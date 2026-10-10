using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string savePath;

    [SerializeField] private string flashlightName = "Flashlight";
    private List<GameObject> allStartingBottlesObjects = new List<GameObject>();
    private List<GameObject> allStartingNotesObjects = new List<GameObject>();
    private List<GameObject> allStartingBoxObjects = new List<GameObject>();
    private List<GameObject> allStartingUpgraders = new List<GameObject>();
    private List<GameObject> allStartingCoresOnGround = new List<GameObject>();
    public List<GameObject> allStartingItemsInWorld = new List<GameObject>();
    private List<GameObject> allStartingChargersInWorld = new List<GameObject>();

    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "gamesave.json");

        GameObject[] bottles = GameObject.FindGameObjectsWithTag("Bottle");
        foreach (GameObject bottle in bottles)
        {
            allStartingBottlesObjects.Add(bottle);
        }

        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");
        foreach (GameObject note in notes)
        {
            allStartingNotesObjects.Add(note);
        }

        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject box in boxes)
        {
            allStartingBoxObjects.Add(box);
        }

        GameObject[] items = GameObject.FindGameObjectsWithTag("Card");
        foreach (GameObject item in items)
        {
            allStartingUpgraders.Add(item);
        }
        GameObject[] groundCores = GameObject.FindGameObjectsWithTag("GeneratorCore"); 
        foreach (GameObject core in groundCores)
        {
            if (core != null) allStartingCoresOnGround.Add(core);
        }
        Inv_Collected[] foundScripts = FindObjectsByType<Inv_Collected>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Inv_Collected script in foundScripts)
        {
            if (script != null)
            {
                
                GameObject toTrack = script.transform.parent != null ? script.transform.parent.gameObject : script.gameObject;
                if (!allStartingItemsInWorld.Contains(toTrack))
                    allStartingItemsInWorld.Add(toTrack);
            }
        }
        Charger[] foundChargers = FindObjectsByType<Charger>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Charger charger in foundChargers)
        {
            if (charger != null)
            {
                GameObject toTrack = charger.transform.parent != null ? charger.transform.parent.gameObject : charger.gameObject;
                if (!allStartingChargersInWorld.Contains(toTrack))
                    allStartingChargersInWorld.Add(toTrack);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) SaveGame();
        if (Input.GetKeyDown(KeyCode.F6)) LoadGame();
    }

    public void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("GameObject with tag 'Player' not found");
            return;
        }
        CharacterMovement cmScript = player.GetComponent<CharacterMovement>();
        Inv_Inventory inventoryScript = FindObjectOfType<Inv_Inventory>();

        SaveData data = new SaveData();

        data.posX = player.transform.position.x;
        data.posY = player.transform.position.y;
        data.posZ = player.transform.position.z;

        data.rotX = player.transform.rotation.x;
        data.rotY = player.transform.rotation.y;
        data.rotZ = player.transform.rotation.z;

        data.collectedBottles.Clear();
        foreach (GameObject bottle in allStartingBottlesObjects)
        {
            if (bottle != null && !bottle.activeSelf)
            {
                data.collectedBottles.Add(bottle.name);
            }
        }

        data.collectedNotes.Clear();
        foreach (GameObject note in allStartingNotesObjects)
        {
            if (note != null && !note.activeSelf)
            {
                data.collectedNotes.Add(note.name);
            }
        }

        data.collectedUpgraders.Clear();
        foreach (GameObject item in allStartingUpgraders)
        {
            if (item != null && !item.activeInHierarchy)
            {
                data.collectedUpgraders.Add(item.name);
            }
        }

        data.openedBoxes.Clear();
        foreach (GameObject box in allStartingBoxObjects)
        {
            if (box != null)
            {
                Animator boxAnim = box.GetComponent<Animator>();
                if (boxAnim != null && boxAnim.GetBool("Open"))
                {
                    data.openedBoxes.Add(box.name);
                }
            }
        }
        data.activeCores.Clear();
        GameObject[] slots = GameObject.FindGameObjectsWithTag("GeneratorSlot");
        foreach (GameObject slot in slots)
        {
            if (slot != null)
            {
                BoxCollider slotCollider = slot.GetComponent<BoxCollider>();
                if (slotCollider != null && slotCollider.enabled == false)
                {
                    data.activeCores.Add(slot.name);
                }
            }
        }
        data.collectedCoresOnGround.Clear();
        foreach (GameObject core in allStartingCoresOnGround)
        {
            if (core != null && !core.activeSelf)
            {
                data.collectedCoresOnGround.Add(core.name); 
            }
        }
        data.chargersNames.Clear();
        data.chargersBattery.Clear();
        Debug.LogError("Liczba ³adowarek do zapisu: " + allStartingChargersInWorld.Count);
        foreach (GameObject chargerObj in allStartingChargersInWorld)
        {
            if (chargerObj != null)
            {
                Charger charger = chargerObj.GetComponentInChildren<Charger>();
                Debug.LogError("£adowarka: " + chargerObj.name + " | bateria: " + charger.batteryInMagazine);
            }
        }
        foreach (GameObject charger in allStartingChargersInWorld)
        {
            if (charger != null)
            {
                data.chargersNames.Add(charger.name);
                int battery = charger.GetComponentInChildren<Charger>().batteryInMagazine;
                data.chargersBattery.Add(battery);
            }
        }
        

        if (cmScript != null)
        {
            data.oxygen = cmScript.oxygen;
            data.cardLevel = cmScript.Level;
            data.helmetPut = cmScript.isPut;
        }

        if (inventoryScript != null)
        {
            data.savedItems = new List<string>(inventoryScript.GetInventoryItems());
        }

        if (inventoryScript != null && inventoryScript.GetInventoryItems().Contains(flashlightName))
        {
            Flashlight flashlightScript = player.GetComponentInChildren<Flashlight>(true); // dodane true
            Debug.LogError("Flashlight znaleziony przy zapisie: " + (flashlightScript != null));
            Debug.LogError("Bateria przy zapisie: " + (flashlightScript != null ? flashlightScript.battery.ToString() : "null"));

            if (flashlightScript != null)
            {
                data.battery = flashlightScript.battery;
            }
            else
            {
                data.battery = 100;
            }
        }
        else
        {
            data.battery = -1;
        }


        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.LogError("ZAWARTOŒÆ PLIKU JSON PRZY ZAPISIE:\n" + json);
        Debug.Log("Saved");
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogError("Brak pliku zapisu!");
            return;
        }
        foreach (GameObject item in allStartingItemsInWorld)
        {
            if (item != null) item.SetActive(true);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        CharacterMovement cmScript = player.GetComponent<CharacterMovement>();
        Inv_Inventory inventoryScript = FindObjectOfType<Inv_Inventory>();

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        if (cmScript != null)
        {
            cmScript.enabled = false;
            player.transform.position = new Vector3(data.posX, data.posY, data.posZ);
            player.transform.rotation = Quaternion.Euler(data.rotX, data.rotY, data.rotZ);
            cmScript.enabled = true;
            cmScript.oxygen = data.oxygen;
            cmScript.Level = data.cardLevel;
            cmScript.isPut = data.helmetPut;
        }


        if (inventoryScript != null)
        {
            
            if (inventoryScript.itemInArm != null)
            {
                Destroy(inventoryScript.itemInArm);
                inventoryScript.itemInArm = null;
            }

            
            for (int i = inventoryScript.playerItems.Count - 1; i >= 0; i--)
            {
                if (inventoryScript.playerItems[i] != null)
                {
                    Destroy(inventoryScript.playerItems[i]);
                }
            }
            inventoryScript.playerItems.Clear(); 

            
            var currentItems = inventoryScript.GetInventoryItems();
            if (currentItems != null)
            {
                currentItems.Clear();
                
                currentItems.AddRange(data.savedItems);
                inventoryScript.RebuildInventoryUI();

            }
        }


        foreach (GameObject bottle in allStartingBottlesObjects)
        {
            if (bottle != null) bottle.SetActive(true);
        }
        foreach (string destroyedName in data.collectedBottles)
        {
            foreach (GameObject bottle in allStartingBottlesObjects)
            {
                if (bottle != null && bottle.name == destroyedName) bottle.SetActive(false);
            }
        }

        
        foreach (GameObject note in allStartingNotesObjects)
        {
            if (note != null) note.SetActive(true);
        }
        foreach (string destroyedNote in data.collectedNotes)
        {
            foreach (GameObject note in allStartingNotesObjects)
            {
                if (note != null && note.name == destroyedNote) note.SetActive(false);
            }
        }

        
        foreach (GameObject item in allStartingUpgraders)
        {
            if (item != null) item.SetActive(true);
        }
        if (data.collectedUpgraders != null)
        {
            foreach (string destroyedItem in data.collectedUpgraders)
            {
                foreach (GameObject item in allStartingUpgraders)
                {
                    if (item != null && item.name == destroyedItem) item.SetActive(false);
                }
            }
        }

       
        foreach (GameObject box in allStartingBoxObjects)
        {
            if (box != null)
            {
                Animator boxAnim = box.GetComponent<Animator>();
                BoxCollider boxCollider = box.GetComponent<BoxCollider>();

                if (boxAnim != null) boxAnim.SetBool("Open", false);
                if (boxCollider != null) boxCollider.enabled = true;
            }
        }
        

        
        foreach (string openedBox in data.openedBoxes)
        {
            foreach (GameObject box in allStartingBoxObjects)
            {
                if (box != null && box.name == openedBox)
                {
                    Animator boxAnim = box.GetComponent<Animator>();
                    BoxCollider boxCollider = box.GetComponent<BoxCollider>();

                    if (boxAnim != null) boxAnim.SetBool("Open", true);
                    if (boxCollider != null) boxCollider.enabled = false;
                }
            }
        }
        foreach (GameObject core in allStartingCoresOnGround)
        {
            if (core != null) core.SetActive(true);
        }
        GameObject[] slots = GameObject.FindGameObjectsWithTag("GeneratorSlot");
        foreach (GameObject slot in slots)
        {
            if (slot != null)
            {
                if (data.activeCores.Contains(slot.name))
                {
                    slot.GetComponent<BoxCollider>().enabled = false;
                    slot.GetComponent<InsertCores>().Core.SetActive(true);
                }
                else
                {
                    slot.GetComponent<BoxCollider>().enabled = true;
                    slot.GetComponent<InsertCores>().Core.SetActive(false);
                }
            }
        }
        foreach (GameObject core in allStartingCoresOnGround)
        {
            if (core != null) core.SetActive(true);
        }

        
        foreach (string coreName in data.collectedCoresOnGround)
        {
            GameObject core = allStartingCoresOnGround.Find(x => x != null && x.name == coreName);
            if (core != null) core.SetActive(false);
        }
        foreach (GameObject chargerObj in allStartingChargersInWorld)
        {
            if (chargerObj != null)
            {
                Charger charger = chargerObj.GetComponentInChildren<Charger>();
                if (charger != null)
                {
                    int index = data.chargersNames.IndexOf(chargerObj.name);

                    if (index != -1)
                    {
                        charger.batteryInMagazine = data.chargersBattery[index];
                        
                        charger.timer = 0f; 
                    }
                }
            }
        }

        if (data.battery == -1) return;

        DeactivateItemsInInventory(data.savedItems);
        if (inventoryScript != null) inventoryScript.RebuildInventoryUI();

        StartCoroutine(SetBatteryAfterStart(data.battery, player));

        Debug.Log("Loaded");






        if (inventoryScript != null)
        {
            inventoryScript.RebuildInventoryUI();
        }

        Flashlight checkFinal = player.GetComponentInChildren<Flashlight>(true);
        Debug.LogError("Bateria na koñcu LoadGame: " + (checkFinal != null ? checkFinal.battery.ToString() : "null"));
        DeactivateItemsInInventory(data.savedItems);
        if (inventoryScript != null)
        {
            inventoryScript.RebuildInventoryUI();
        }
        Debug.Log("Loaded");
    }
    public void RegisterSpawnedItem(GameObject newItem)
    {
        if (newItem != null && !allStartingUpgraders.Contains(newItem))
        {
            allStartingUpgraders.Add(newItem);
            
        }
    }
    private void DeactivateItemsInInventory(List<string> savedItems)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (string itemName in savedItems)
        {
            foreach (GameObject obj in allStartingItemsInWorld)
            {
                if (obj == null) continue;

                Inv_Collected collected = obj.GetComponentInChildren<Inv_Collected>(true);
                if (collected != null && collected.itemName == itemName)
                {
                    Debug.LogError("Znalaz³em: " + obj.name + " | œcie¿ka: " + GetPath(obj.transform) + " | jest dzieckiem gracza: " + obj.transform.IsChildOf(player.transform));

                    if (!obj.transform.IsChildOf(player.transform))
                    {
                        obj.SetActive(false);
                    }
                    break;
                }
            }
        }
    }
    private IEnumerator SetBatteryAfterStart(int batteryValue, GameObject player)
    {
        Flashlight lastFlashlight = null;

        while (true)
        {
            yield return null;

            Flashlight current = player.GetComponentInChildren<Flashlight>(true);

            if (current != null && current != lastFlashlight)
            {
                lastFlashlight = current;
                current.battery = batteryValue;
                current.BatteryTextUpdate();
                Debug.LogError("Bateria ustawiona: " + current.battery);
            }
        }
    }
    private string GetPath(Transform t)
    {
        string path = t.name;
        Transform current = t;
        while (current.parent != null)
        {
            current = current.parent;
            path = current.name + "/" + path;
        }
        return path;
    }
}