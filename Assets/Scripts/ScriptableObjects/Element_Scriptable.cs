using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type{Drink, Eat, Sleep, Life, Pruebas};

[CreateAssetMenu(fileName ="Element", menuName ="NewElement")]
public class Element_Scriptable : ScriptableObject
{
    [Header("Info")]
    public string name;
    public string description;

    public Type elementType;
    public int amount;
    public Sprite icon;
    public GameObject prefabObject;
    
}
