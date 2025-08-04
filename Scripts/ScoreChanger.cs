using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreChanger : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI easy;
    public TextMeshProUGUI med;
    public TextMeshProUGUI hard;
    public int ES1, ES2, ES3, ES4, MS1, MS2, MS3, MS4, HS1, HS2, HS3, HS4;

    // Start is called before the first frame update
    void Start()
    {
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
        if(PlayerPrefs.GetString("Song","") == "music1")
        {
            easy.text = ES1.ToString();
            med.text = MS1.ToString();
            hard.text = HS1.ToString();
        }
        else if (PlayerPrefs.GetString("Song", "") == "music2")
        {
            easy.text = ES2.ToString();
            med.text = MS2.ToString();
            hard.text = HS2.ToString();
        }
        else if (PlayerPrefs.GetString("Song", "") == "music3")
        {
            easy.text = ES3.ToString();
            med.text = MS3.ToString();
            hard.text = HS3.ToString();
        }
        else
        {
            easy.text = ES4.ToString();
            med.text = MS4.ToString();
            hard.text = HS4.ToString();
        }
    }
}
