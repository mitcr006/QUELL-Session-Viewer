using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleJSON;
using System;

public class SessionOverviewObject : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private TextMeshProUGUI quellScore;

    [SerializeField]
    private TextMeshProUGUI timeAndDate;

    private float power, accuracy, pace, defense;

    [SerializeField]
    private PiGenerator piChart;

    public void PopulateData(JSONNode sessionData, int idInArray)
    {
        id = idInArray;
        int quellScoreInt = (int)sessionData["QuellScore"]["Score"];
        quellScore.text = "" + quellScoreInt.ToString("N0");
        timeAndDate.text = DateTime.Parse(sessionData["SessionStart"]).ToString("HH:mm dd/MM/yy");
        power = sessionData["QuellScore"]["PowerScore"];
        accuracy = sessionData["QuellScore"]["AccuracyScore"];
        pace = sessionData["QuellScore"]["PaceScore"];
        defense = sessionData["QuellScore"]["DefenceScore"];
        piChart.segmantValues[0] = power;
        piChart.segmantValues[1] = accuracy;
        piChart.segmantValues[2] = pace;
        piChart.segmantValues[3] = defense;
        piChart.UpdatePi();
    }

    public void SelectSession()
    {
        FindObjectOfType<SessionManager>().SelectSession(id);
    }
}
