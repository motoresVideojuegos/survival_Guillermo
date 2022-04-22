using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interfaces;

public class DamageOverTime : MonoBehaviour
{
    public int dmgAmount;
    public float index_deterioration;
    bool canDeteriorate;

    List<IDeterioration> deteriorationList = new List<IDeterioration>();

    // Start is called before the first frame update
    void Start()
    {
        canDeteriorate = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (canDeteriorate)
        {
            StartCoroutine(DeteriorationHandle());
        }*/
    }

    IEnumerator DeteriorationHandle()
    {
        canDeteriorate = false;

        for (int i = 0; i < deteriorationList.Count; i++)
        {
            deteriorationList[i].SetDeterioration(dmgAmount);
        }
        yield return new WaitForSeconds(index_deterioration);
        canDeteriorate = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDeterioration>() != null)
        {
            deteriorationList.Add(other.GetComponent<IDeterioration>());
            if(canDeteriorate){
                StartCoroutine(DeteriorationHandle());
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.GetComponent<IDeterioration>() != null)
        {
            if(canDeteriorate){
                StartCoroutine(DeteriorationHandle());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IDeterioration>() != null)
        {
            deteriorationList.Remove(other.GetComponent<IDeterioration>());
        }
    }
}
