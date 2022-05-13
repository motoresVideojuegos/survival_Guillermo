using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Classes;

public class Slot_UIscript : MonoBehaviour
{

    public Button button;
    public Image img;
    public TextMeshProUGUI text;
    SlotElement slot_current;

    public int index;

    public void AssignElement(SlotElement slot)
    {
        slot_current = slot;
        img.gameObject.SetActive(true);
        img.sprite = slot.element.icon;
        text.text = slot.amount > 1 ? slot.amount.ToString() : string.Empty;
    }

    public void Clean()
    {
        slot_current = null;

        img.gameObject.SetActive(false);
        text.text = string.Empty;

    }

    public void OnButtonClick()
    {
        Slot_PlayerScript.inst.SelectedElement(index);
    }

}
