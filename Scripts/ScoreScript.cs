using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR;

public class ScoreScript : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI txt;
    private int ES1, ES2, ES3, ES4, MS1, MS2, MS3, MS4, HS1, HS2, HS3, HS4;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        ES1 = PlayerPrefs.GetInt("ES1", 0);
        ES2 = PlayerPrefs.GetInt("ES2", 0);
        ES3 = PlayerPrefs.GetInt("ES3", 0);
        ES4 = PlayerPrefs.GetInt("ES4", 0);
        MS1 = PlayerPrefs.GetInt("MS1", 0);
        MS2 = PlayerPrefs.GetInt("MS2", 0);
        MS3 = PlayerPrefs.GetInt("MS3", 0);
        MS4 = PlayerPrefs.GetInt("MS4", 0);
        HS1 = PlayerPrefs.GetInt("HS1", 0);
        HS2 = PlayerPrefs.GetInt("HS2", 0);
        HS3 = PlayerPrefs.GetInt("HS3", 0);
        HS4 = PlayerPrefs.GetInt("HS4", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("Song", "") == "music1" && PlayerPrefs.GetString("Diff", "") == "easy")
        {
            if (score > ES1)
            {
                ES1 = score;
                PlayerPrefs.SetInt("ES1", ES1);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music2" && PlayerPrefs.GetString("Diff", "") == "easy")
        {
            if (score > ES2)
            {
                ES2 = score;
                PlayerPrefs.SetInt("ES2", ES2);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music3" && PlayerPrefs.GetString("Diff", "") == "easy")
        {
            if (score > ES3)
            {
                ES3 = score;
                PlayerPrefs.SetInt("ES3", ES3);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music4" && PlayerPrefs.GetString("Diff", "") == "easy")
        {
            if (score > ES4)
            {
                ES4 = score;
                PlayerPrefs.SetInt("ES4", ES4);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music1" && PlayerPrefs.GetString("Diff", "") == "med")
        {
            if (score > MS1)
            {
                MS1 = score;
                PlayerPrefs.SetInt("MS1", MS1);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music2" && PlayerPrefs.GetString("Diff", "") == "med")
        {
            if (score > MS2)
            {
                MS2 = score;
                PlayerPrefs.SetInt("MS2", MS2);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music3" && PlayerPrefs.GetString("Diff", "") == "med")
        {
            if (score > MS3)
            {
                MS3 = score;
                PlayerPrefs.SetInt("MS3", MS3);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music4" && PlayerPrefs.GetString("Diff", "") == "med")
        {
            if (score > MS4)
            {
                MS4 = score;
                PlayerPrefs.SetInt("MS4", MS4);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music1" && PlayerPrefs.GetString("Diff", "") == "hard")
        {
            if (score > HS1)
            {
                HS1 = score;
                PlayerPrefs.SetInt("HS1", HS1);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music2" && PlayerPrefs.GetString("Diff", "") == "hard")
        {
            if (score > HS2)
            {
                HS2 = score;
                PlayerPrefs.SetInt("HS2", HS2);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music3" && PlayerPrefs.GetString("Diff", "") == "hard")
        {
            if (score > HS3)
            {
                HS3 = score;
                PlayerPrefs.SetInt("HS3", HS3);
            }
        }
        else if (PlayerPrefs.GetString("Song", "") == "music4" && PlayerPrefs.GetString("Diff", "") == "hard")
        {
            if (score > HS4)
            {
                HS4 = score;
                PlayerPrefs.SetInt("HS4", HS4);
            }
        }
        txt.text = score.ToString();
    }
}
