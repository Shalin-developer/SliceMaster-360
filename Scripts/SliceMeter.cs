using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliceMeter : MonoBehaviour
{
    public float sliceMeter = 100;
    public Slider sliceMeterSlider; // Reference to the UI Slider
    public spawnFruits spawner;
    float decreaseRate=10f;
    public int score=0;
    public TMP_Text scoreText;
    public int highScore;

    void Start()
    {
        // Ensure the slider starts at the correct value
        if (sliceMeterSlider != null)
            sliceMeterSlider.value = sliceMeter;
        highScore = PlayerPrefs.GetInt("HighScore1", 0);
    }

    void Update()
    {
        if (!spawner.gameOver)
        {
            if (sliceMeter > 0)
            {
                sliceMeter -= Time.deltaTime * decreaseRate;
                sliceMeter = Mathf.Clamp(sliceMeter, 0, 100); // Ensure value stays between 0 and 100

                if (sliceMeterSlider != null)
                    sliceMeterSlider.value = sliceMeter;
            }
            else
            {
                spawner.GameOver();
            }
        }
        decreaseRate += Time.deltaTime * 0.05f;
    }

    public void Sliced()
    {
        if (!spawner.gameOver)
        {
            sliceMeter += 12f;
            sliceMeter = Mathf.Clamp(sliceMeter, 0, 100); // Prevent exceeding max value

            if (sliceMeterSlider != null)
                sliceMeterSlider.value = sliceMeter;
        }
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
        if (score > 1000)
        {
            scoreText.text = score.ToString();
        }
        else if (score > 100)
        {
            scoreText.text = "0" + score.ToString();
        }
        else
        {
            scoreText.text = "00" + score.ToString();
        }
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore1", highScore);
        }
    }



}
