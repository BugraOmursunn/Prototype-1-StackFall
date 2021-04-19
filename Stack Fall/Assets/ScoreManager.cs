using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int highScore;
    public Text HighScoreText;
    public int Score;
    public Text ScoreText;

    private void Awake()
    {
        makeSingleton();
    }
    private void makeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
            PlayerPrefs.SetInt("HighScore", 0);


    }
    public void SetHighScore()
    {
        if (HighScoreText == null)
            HighScoreText = GameObject.Find("HighScore").GetComponent<Text>();

        highScore = PlayerPrefs.GetInt("HighScore");
        HighScoreText.text = highScore.ToString();
    }
    public void AddScore(int value)
    {
        if (ScoreText == null)
            ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        if (value == 0)//reset score
        {
            Score = 0;
            ScoreText.text = Score.ToString();
            return;
        }

        highScore = PlayerPrefs.GetInt("HighScore");
        Score += value;

        if (Score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
        ScoreText.text = Score.ToString();
    }
    public void ResetScore()
    {
        Score = 0;
    }
}
