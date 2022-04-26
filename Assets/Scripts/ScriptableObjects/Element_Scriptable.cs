using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Element", menuName ="NewElement")]
public class Element_Scriptable : ScriptableObject
{
    [Header("Info")]
    public string name;
    public string description;

    public Sprite icon;
    public GameObject prefabObject;
    
}
