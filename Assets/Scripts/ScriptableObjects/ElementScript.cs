using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interfaces;

public class ElementScript : MonoBehaviour, IInteractable
{

    public Element_Scriptable element;

    public string GetMessageInteractable()
    {
        return string.Format("[E] Obtener {0}", element.name);
    }

    public void OnInteract()
    {
        Destroy(gameObject);
    }
}
