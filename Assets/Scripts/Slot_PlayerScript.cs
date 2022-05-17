using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Classes;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Slot_PlayerScript : MonoBehaviour
{
    IndicatorScript indicatorScript;
    int maxAmount;

    public Slot_UIscript[] slotsUI;
    public SlotElement[] slotElement;

    public GameObject inventoryWindow;
    public Transform positionDrop;

    [Header("Selected Item")]
    SlotElement slot_selectedItem;
    int index_SelectedItem;
    public TextMeshProUGUI text_selectedItem;
    public TextMeshProUGUI description_selectedItem;
    //public TextMeshProUGUI statsName_selectedItem;
    //public TextMeshProUGUI statsNumber_selectedItem;
    public TextMeshProUGUI inventoryFull_text;
    public GameObject useButton;
    public GameObject dropButton;

    PlayerController playerScript;

    public static Slot_PlayerScript inst;
    private void Awake()
    {
        maxAmount = 3;
        //singleton
        if (Slot_PlayerScript.inst == null)
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        playerScript = GetComponent<PlayerController>();
        indicatorScript = GetComponent<IndicatorScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryWindow.SetActive(false);
        slotElement = new SlotElement[slotsUI.Length];

        for (int i = 0; i < slotElement.Length; i++)
        {
            slotElement[i] = new SlotElement();
            slotsUI[i].index = i;
            slotsUI[i].Clean();
        }
    }
    private void Update()
    {

    }

    public bool CheckInventory()
    {
        return slotElement[slotElement.Length - 1].element != null &&
            slotElement[slotElement.Length - 1].amount >= maxAmount;
    }

    public void AddElement(Element_Scriptable element)
    {

        SlotElement elementToStore = GetStoredElement(element);
        if (elementToStore != null)
        {
            if (elementToStore.amount < maxAmount)
            {
                elementToStore.amount++;
                UpdateUI();
                return; //------------------------------D
            }

        }

        SlotElement emptyElement = GetEmptySpace();
        if (emptyElement != null)
        {
            emptyElement.element = element;
            emptyElement.amount = 1;
            UpdateUI();
            return; //------------------------------
        }

        DropElement(element);
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slotElement.Length; i++)
        {
            if (slotElement[i].element != null)
            {
                slotsUI[i].AssignElement(slotElement[i]);
            }
            else
            {
                slotsUI[i].Clean();
            }
        }
    }

    SlotElement GetStoredElement(Element_Scriptable element)
    {
        for (int i = 0; i < slotElement.Length; i++)
        {
            if (slotElement[i].element == element && slotElement[i].amount < maxAmount)
            {
                return slotElement[i];
            }
        }
        return null;
    }

    public void DropElement(Element_Scriptable element)
    {
        Instantiate(element.prefabObject, positionDrop.position, Quaternion.identity);
    }

    SlotElement GetEmptySpace()
    {
        for (int i = 0; i < slotElement.Length; i++)
        {
            if (slotElement[i].element == null)
            {
                return slotElement[i];
            }
        }

        return null;
    }

    public void SelectedElement(int index)
    {
        if (slotElement[index].element != null)
        {
            slot_selectedItem = slotElement[index];
            index_SelectedItem = index;

            text_selectedItem.text = slot_selectedItem.element.name;
            description_selectedItem.text = slot_selectedItem.element.description;

            useButton.SetActive(true);
            dropButton.SetActive(true);
        }
    }

    public void CleanWindowSelectedElement()
    {
        slot_selectedItem = null;

        text_selectedItem.text = string.Empty;
        description_selectedItem.text = string.Empty;

        useButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnButton_Inventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OpenClose_Inventory();
        }
    }

    public void OpenClose_Inventory()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            playerScript.ActivateDeactivate_mousePointer(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
            CleanWindowSelectedElement();
            playerScript.ActivateDeactivate_mousePointer(true);
        }
    }

    public void OnUseButton()
    {
        switch (slot_selectedItem.element.elementType)
        {
            case Type.Drink:
                indicatorScript.Drink(slot_selectedItem.element.amount);
                break;
            case Type.Eat:
                indicatorScript.Eat(slot_selectedItem.element.amount);
                break;
            case Type.Sleep:
                indicatorScript.Sleep();
                break;
            case Type.Life:
                indicatorScript.AddLife(slot_selectedItem.element.amount);
                break;
        }
        RemoveSelectedElement();
    }

    public void AddSleep()
    {
        indicatorScript.Sleep();
    }

    public void OnDropButton()
    {
        DropElement(slot_selectedItem.element);
        RemoveSelectedElement();
    }

    public void RemoveSelectedElement()
    {
        slot_selectedItem.amount--;
        if (slot_selectedItem.amount == 0)
        {
            slot_selectedItem.element = null;
            CleanWindowSelectedElement();
        }
        UpdateUI();
    }
}
