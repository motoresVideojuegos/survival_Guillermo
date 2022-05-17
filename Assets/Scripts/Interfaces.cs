using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces : MonoBehaviour
{
    public interface IDeterioration{
        void SetDeterioration(float amount);
    }

    public interface IInteractable{
        string GetMessageInteractable();
        void OnInteract();
        Element_Scriptable GetElement();
    }
}
