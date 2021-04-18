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
        public GameObject DiedPanel;
        public GameObject EndPanel;
    }
    [SerializeField] private Panels _panels;
    [SerializeField] private Text LevelText;
    private
    void Start()
    {
        LevelText.text = PlayerPrefs.GetInt("Level").ToString();
        SetUIState(0);
    }

    public void SetUIState(int value)
    {
        _panels.StartPanel.SetActive(false); _panels.DiedPanel.SetActive(false); _panels.EndPanel.SetActive(false);

        switch (value)
        {
            case 0:
                _panels.StartPanel.SetActive(true);
                break;
            case 1:
                break;
            case 2:
                _panels.DiedPanel.SetActive(true);
                break;
            case 3:
                _panels.EndPanel.SetActive(true);
                break;
        }
    }
}
