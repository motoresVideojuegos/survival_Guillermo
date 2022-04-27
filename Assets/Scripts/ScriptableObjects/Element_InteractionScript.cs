using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Element_InteractionScript : MonoBehaviour
{
    public TextMeshProUGUI TMPtext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<ElementScript>()){
            TMPtext.text = other.gameObject.GetComponent<ElementScript>().GetMessageInteractable();
            TMPtext.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        TMPtext.gameObject.SetActive(false);
    }
}
