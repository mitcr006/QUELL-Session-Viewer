using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserProfileObject : MonoBehaviour
{
    public string saveFileLocation;
    public TextMeshProUGUI username;

    public void ChooseProfile()
    {
        FindObjectOfType<SessionManager>().LoadSave(saveFileLocation);
    }
}
