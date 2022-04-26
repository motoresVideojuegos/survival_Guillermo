using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classes : MonoBehaviour
{

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
