using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Element_InteractionScript : MonoBehaviour
{
    public TextMeshProUGUI TMPtext;
    [SerializeField]
    private GameObject interactGO;

    public Slot_PlayerScript slot_player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<ElementScript>())
        {
            interactGO = other.gameObject;
            TMPtext.text = other.gameObject.GetComponent<ElementScript>().GetMessageInteractable();
            TMPtext.gameObject.SetActive(true);

        }
    }



    private void OnTriggerExit(Collider other)
    {
        TMPtext.gameObject.SetActive(false);
        interactGO = null;
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if(interactGO != null){
                if(interactGO.GetComponent<ElementScript>().element.elementType != Type.Sleep){
                    slot_player.AddElement(interactGO.GetComponent<ElementScript>().element);
                    interactGO.GetComponent<ElementScript>().OnInteract();
                    TMPtext.gameObject.SetActive(false);
                    interactGO = null;
                }else{
                    interactGO.GetComponent<ElementScript>().OnInteract();
                }
                
            }
            
        }

    }
}
