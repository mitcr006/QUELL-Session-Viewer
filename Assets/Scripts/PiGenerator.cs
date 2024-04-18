using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiGenerator : MonoBehaviour
{
    public Image[] piSegmants;
    public float[] segmantValues;
    // Start is called before the first frame update
    void Start()
    {
        SetValues(segmantValues);
    }

    public void UpdatePi()
    {
        SetValues(segmantValues);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValues(float[] valuesToSet)
    {
        float totalValues = 0;
        for (int i = 0; i < piSegmants.Length; i++)
        {
            totalValues += FindPercentage(valuesToSet, i);
            piSegmants[i].fillAmount = totalValues;
        }
    }

    private float FindPercentage(float[] valueToSet, int index)
    {
        float totalAmount = 0;
        for(int i = 0; i < valueToSet.Length; i++)
        {
            totalAmount += valueToSet[i];
        }

        return valueToSet[index] / totalAmount;
    }
}
