using System.IO;
using UnityEngine;
using SimpleJSON;
using TMPro;
using System;
using UnityEngine.UI;

public class SessionManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI userName;

    public string saveDataFilePath;
    public JSONNode rawSaveNode;
    private JSONNode sessionsData;
    private JSONNode selectedSession;


    [SerializeField]
    private GameObject sessionPrefab, sessionsList, sessionBreakdown, sessionLoader, userProfileObject;

    [SerializeField]
    private Transform sessionParent, userParent;

    [SerializeField]
    private TextMeshProUGUI sessionTitle;

    [SerializeField]
    private InputField playerDataField;

    public JSONNode SelectedSession { get => selectedSession; }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        saveDataFilePath = GetDirPath();

        string[] paths = Application.persistentDataPath.Split("/");
        foreach (string part in paths)
            Debug.Log(part);
        

        DirectoryInfo quellUsers = new DirectoryInfo(saveDataFilePath);
        foreach (var folder in quellUsers.GetDirectories())
        {
            Debug.Log(folder);
            DirectoryInfo quellProfiles = new DirectoryInfo(folder.FullName + "/Profiles/");
            foreach (var profileFolder in quellProfiles.GetDirectories())
            {
                Debug.Log(profileFolder);
                JSONNode user = JSON.Parse(File.ReadAllText(profileFolder.FullName + "/info.json"));
                Debug.Log("We found the User: " + user["data"]["nickname"]);
                GameObject userProfileButton = Instantiate(userProfileObject, userParent);
                userProfileButton.GetComponent<UserProfileObject>().saveFileLocation = profileFolder.FullName + "/Shardfall/save_data/PlayerData.json";
                userProfileButton.GetComponent<UserProfileObject>().username.text = user["data"]["nickname"];
            }

        }
    }

    public string GetDirPath() {
        if (Application.platform == RuntimePlatform.OSXEditor){
            DirectoryInfo parentPath = Directory.GetParent(Application.persistentDataPath.ToString());
            DirectoryInfo parent2 = Directory.GetParent(parentPath.FullName);
            return parent2.FullName + "/com.Quell.Shardfall/Users/";
        }
        DirectoryInfo parent = Directory.GetParent(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData));
        return parent.FullName+"/LocalLow/Quell/Users";
       
    }

    public void TryParseInput()
    {
        if (InputValid())
        {
            sessionLoader.SetActive(false);
            sessionsList.SetActive(true);
            userName.text = rawSaveNode["PlayerName"];
            sessionsData = rawSaveNode["Sessions"];
            Debug.Log("I have done " + sessionsData.Count + " Sessions");
            int i = 0;
            foreach (JSONNode session in sessionsData)
            {
                GameObject sessionObj = Instantiate(sessionPrefab, sessionParent);
                sessionObj.GetComponent<SessionOverviewObject>().PopulateData(session, i);
                i++;
            }
        }
        else
        {
            Debug.LogError("There is something wrong with this input.");
        }
    }

    private bool InputValid()
    {
        return rawSaveNode["PlayerName"] != null && rawSaveNode["PlayerID"] != null && rawSaveNode["WorldLevel"] != null && rawSaveNode["Sessions"] != null;
    }



    public bool LoadSave(string filePath)
    {
        if (File.Exists(filePath))
        {
            //Load the data
            rawSaveNode = JSON.Parse(File.ReadAllText(filePath));
            TryParseInput();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SelectSession(int id)
    {
        selectedSession = sessionsData[id];
        sessionsList.SetActive(false);
        sessionBreakdown.SetActive(true);
        sessionTitle.text = DateTime.Parse(SelectedSession["SessionStart"]).ToString("HH:mm dd/MM/yy");
    }


}
