using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleJSON;

public class SessionDetailPrefab : MonoBehaviour
{
    [SerializeField]
    private string associatedKey;

    [SerializeField]
    private string subKey;

    [SerializeField]
    private TextMeshProUGUI valueText, nameText;

    [SerializeField]
    private bool isBarChart = false;

    [SerializeField]
    private GameObject barPrefab;

    [SerializeField]
    private Transform barParent;

    [SerializeField]
    private SessionManager sessionManager;

    private void OnEnable()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        AssignValue();
    }

    public void SetValueText(float value)
    {
        valueText.text = "" + value;
    }

    public void DeteleObject()
    {
        Destroy(this.gameObject);
    }

    public void SetupObject(string _key, string _subkey, string valueName)
    {
        nameText.text = valueName;
        subKey = _subkey;
        associatedKey = _key;
        AssignValue();
    }

    private void AssignValue()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        if (!isBarChart)
        {
            if (subKey != "")
                valueText.text = ((int)sessionManager.SelectedSession[subKey][associatedKey]).ToString("N0");
            else
                valueText.text = ((int)sessionManager.SelectedSession[associatedKey]).ToString("N0");
        }
        else
        {
            foreach (Transform bar in barParent)
            {
                Destroy(bar.gameObject);
            }

            for (int i = 0; i < sessionManager.SelectedSession["CaloriesBurnedPerMinute"].Count; i++)
            {
                Debug.Log(sessionManager.SelectedSession["CaloriesBurnedPerMinute"][i][associatedKey]);
                GameObject barInChart = Instantiate(barPrefab, barParent);
                float height = sessionManager.SelectedSession["CaloriesBurnedPerMinute"][i][associatedKey];
                barInChart.GetComponent<RectTransform>().sizeDelta = new Vector2(50f, (height * 3.6f));
                barInChart.GetComponent<BarValueSetter>().sessionObjectParent = this;
                barInChart.GetComponent<BarValueSetter>().associatedValue = height;
            }
        }
    }
}
