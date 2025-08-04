using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CastleLevelManager : MonoBehaviour
{
    public int health = 100;
    public Slider healthSlider;
    public GameObject gameOverScreen;
    public spawnFruits spawner;
    public int score=0;
    public Text scoreText;
    public int highScore;
    public PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = health;
        highScore = PlayerPrefs.GetInt("HighScore3", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHealth(int decrease)
    {
        health -= decrease;
        healthSlider.value = health;
        if (health <= 0)
        {
            spawner.gameOver = true;
            StartCoroutine(GameOver());
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
            scoreText.text = "0"+score.ToString();
        }
        else
        {
            scoreText.text = "00" + score.ToString();
        }
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore3", highScore);
        }
    }

    IEnumerator GameOver()
    {
        pauseMenu.gameOver = true;
        gameOverScreen.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Castle");
    }
}
