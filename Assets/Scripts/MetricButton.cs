using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetricButton : MonoBehaviour
{
    [SerializeField]
    private string subkey, key;

    [SerializeField]
    private bool isPerMinuteValue;

    [SerializeField]
    private GameObject perMinPrefab, singleValuePrefab;

    [SerializeField]
    private Transform parent, AddMenu;

    public void OnClicked()
    {
        GameObject objectInList = new GameObject();
        if (isPerMinuteValue)
        {
            objectInList = Instantiate(perMinPrefab, parent);
        }
        else
        {
            objectInList = Instantiate(singleValuePrefab, parent);
        }

        SessionDetailPrefab objectProperties = objectInList.GetComponent<SessionDetailPrefab>();
        objectProperties.SetupObject(key, subkey, transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text);
        AddMenu.gameObject.SetActive(false);

    }
}
