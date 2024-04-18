using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoreInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemsToToggle;

    [SerializeField]
    private bool showing;

    [SerializeField]
    private Image showMoreIcon;

    public void Toggle()
    {
        showing = !showing;
        foreach (GameObject item in itemsToToggle)
        {
            item.SetActive(showing);
        }

        if (showing)
        {
            showMoreIcon.gameObject.transform.eulerAngles = new Vector3(0, 0, -90f);
        }
        else
        {
            showMoreIcon.gameObject.transform.eulerAngles = new Vector3(0, 0, 90f);
        }
    }
}
