using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [System.Serializable]
    public class Panels
    {
        public GameObject StartPanel;
        public GameObject PlayingPanel;
        public GameObject DiedPanel;
        public GameObject EndPanel;
    }
    [SerializeField] private Panels _panels;
    [SerializeField] private Text CurrentLevelText;
    [SerializeField] private Text NextLevelText;
    [SerializeField] private Image CurrentLevelFg, NextLevelFg, LevelProgressImage;


    private Color _color;

    private
    void Start()
    {
        CurrentLevelText.text = PlayerPrefs.GetInt("Level").ToString();
        NextLevelText.text = (PlayerPrefs.GetInt("Level") + 1).ToString();
        SetUIState(0);

        _color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
        LevelProgressImage.color = _color;
        CurrentLevelFg.color = _color;
        NextLevelFg.color = _color;

    }

    public void SetUIState(int value)
    {
        _panels.StartPanel.SetActive(false); _panels.PlayingPanel.SetActive(false); _panels.DiedPanel.SetActive(false); _panels.EndPanel.SetActive(false);

        switch (value)
        {
            case 0:
                _panels.StartPanel.SetActive(true);
                ScoreManager.instance.SetHighScore();
                break;
            case 1:
                _panels.PlayingPanel.SetActive(true);
                break;
            case 2:
                _panels.DiedPanel.SetActive(true);
                break;
            case 3:
                _panels.EndPanel.SetActive(true);
                break;
        }
    }
    public void ProgressLevelBar(float value)
    {
        LevelProgressImage.fillAmount = value;
    }
}
