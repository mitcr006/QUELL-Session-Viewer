using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarValueSetter : MonoBehaviour
{
    [SerializeField]
    private Color selected;

    [SerializeField]
    private Color notSelected;

    public float associatedValue;

    public SessionDetailPrefab sessionObjectParent;

    public void Select()
    {
        foreach (Transform bar in transform.parent)
        {
            bar.gameObject.GetComponent<BarValueSetter>().SetInactive();
        }
        SetActive();
    }

    public void SetInactive()
    {
        GetComponent<Image>().color = notSelected;
    }

    public void SetActive()
    {
        GetComponent<Image>().color = selected;
        sessionObjectParent.SetValueText(associatedValue);

    }
}
