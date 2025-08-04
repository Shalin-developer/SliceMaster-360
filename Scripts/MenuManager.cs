using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public int highScore1, highScore2, highScore3;
    public TMP_Text hsText1, hsText2, hsText3;
    // Start is called before the first frame update
    void Start()
    {

        highScore1 = PlayerPrefs.GetInt("HighScore1", 0);
        highScore2 = PlayerPrefs.GetInt("HighScore2", 0);
        highScore3 = PlayerPrefs.GetInt("HighScore3", 0);
        SetHighScoreText(highScore1, hsText1);
        SetHighScoreText(highScore2, hsText2);
        SetHighScoreText(highScore3, hsText3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetHighScoreText(int highScore, TMP_Text hsText)
    {
        if (highScore > 1000)
        {
            hsText.text = highScore.ToString();
        }
        else if (highScore > 100)
        {
            hsText.text = "0" + highScore.ToString();
        }
        else if (highScore > 10)
        {
            hsText.text = "00" + highScore.ToString();
        }
        else
        {
            hsText.text = "000" + highScore.ToString();
        }
    }
}
