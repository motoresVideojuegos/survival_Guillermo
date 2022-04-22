using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using static Interfaces;

public class IndicatorScript : MonoBehaviour, IDeterioration
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

        if (life.currentValue <= 0)
        {
            Death();
        }
        else
        {
            if (Timer >= 1)
            {
                Timer = 0f;
                if (relaxation.currentValue <= 0)
                {
                    hunger.Remove(hunger.index_deterioration * 2);
                    dehydration.Remove(dehydration.index_deterioration * 2);
                }
                else
                {
                    if (hunger.currentValue > 0)
                    {
                        hunger.Remove(hunger.index_deterioration);
                    }

                    if (dehydration.currentValue > 0)
                    {
                        dehydration.Remove(dehydration.index_deterioration);
                    }

                    if (relaxation.currentValue > 0)
                    {
                        relaxation.Remove(relaxation.index_deterioration);
                    }
                }

                if (hunger.currentValue <= 0)
                {
                    life.Remove(hunger.index_deterioration_life);
                }

                if (dehydration.currentValue <= 0)
                {
                    life.Remove(dehydration.index_deterioration_life);
                }

            }
        }

        life.UIimage.fillAmount = life.GetPercentage();
        hunger.UIimage.fillAmount = hunger.GetPercentage();
        dehydration.UIimage.fillAmount = dehydration.GetPercentage();
        relaxation.UIimage.fillAmount = relaxation.GetPercentage();

    }

    public void AddLife(float num)
    {
        life.Add(num);
    }

    public void Eat(float num)
    {
        hunger.Add(num);
    }

    public void Drink(float num)
    {
        dehydration.Add(num);
    }

    public void Sleep(float num)
    {
        relaxation.Add(num);
    }

    public void Death()
    {
        Debug.Log("Dead");
    }

    public void SetDeterioration(float amount)
    {
        life.Remove(amount);
        onGetDeterioration.Invoke();
    }

    [System.Serializable]
    public class Indicator
    {

        public float currentValue;
        public float initialValue;
        public float maxValue;
        public float index_recuperation;
        public float index_deterioration;
        public float index_deterioration_life;
        public Image UIimage;

        public void Add(float num)
        {
            currentValue = Mathf.Min(currentValue + num, maxValue);
        }

        public void Remove(float num)
        {
            currentValue = Mathf.Max(currentValue - num, 0.0f);
        }

        public float GetPercentage()
        {
            return currentValue / maxValue;
        }
    }
}
