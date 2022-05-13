using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interfaces;

public class ElementScript : MonoBehaviour, IInteractable
{

    public Element_Scriptable element;

    public string GetMessageInteractable()
    {
        if(element.elementType == Type.Sleep){
            return string.Format("[E] Usar {0}", element.name);
        }else{
            return string.Format("[E] Obtener {0}", element.name);
        }
        
    }

    public void OnInteract()
    {
        if(element.elementType == Type.Sleep){
            Slot_PlayerScript.inst.AddSleep();
        }else{
            Destroy(gameObject);
        }
       
    }
}
