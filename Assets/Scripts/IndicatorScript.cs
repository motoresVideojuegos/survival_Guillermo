using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class IndicatorScript : MonoBehaviour
{
    public Indicator life;
    public Indicator hunger;
    public Indicator dehydration;
    public Indicator relaxation;

    public float deterioration_hunger_life;
    public float deterioration_dehydration_life;

    public UnityEvent onGetDeterioration;

    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        life.currentValue = life.initialValue;
        hunger.currentValue = hunger.initialValue;
        dehydration.currentValue = dehydration.initialValue;
        relaxation.currentValue = relaxation.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;

        if(Timer >= 1){
            Timer = 0f;
            if(relaxation.currentValue <= 0){
                hunger.Remove(hunger.index_deterioration * 2);
                dehydration.Remove(dehydration.index_deterioration * 2);
            }else {
                hunger.Remove(hunger.index_deterioration);
                dehydration.Remove(dehydration.index_deterioration);
            }
            

        }

    }

    public void AddLife(float num){
        life.Add(num);
    }

    public void Eat(float num){
        hunger.Add(num);
    }

    public void Drink(float num){
        dehydration.Add(num);
    }

    public void Sleep(float num){
        relaxation.Add(num);
    }

    public void Death(){
        Debug.Log("Dead");
    }

    [System.Serializable]
    public class Indicator{

        public float currentValue;
        public float initialValue;
        public float maxValue;
        public float index_recuperation;
        public float index_deterioration;
        public Image UIimage;

        public void Add(float num){
            currentValue = Mathf.Min(currentValue + num, maxValue);
        }

        public void Remove(float num){
            currentValue = Mathf.Max(currentValue - num, 0.0f);
        }

        public float GetPercentage(){
            return currentValue / maxValue;
        }
    }
}
