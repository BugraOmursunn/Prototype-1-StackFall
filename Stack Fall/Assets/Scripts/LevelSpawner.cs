using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
public class LevelSpawner : MonoBehaviour
{
    [Serializable]
    public class obstacleTypes
    {
        public string name;
        public GameObject[] obstacleModels;
    }
    public List<obstacleTypes> obstacleList;

    public Gradient _gradientColor;
    //[HideInInspector] 
    public GameObject[] obstacleGameobjects;
    public GameObject winPrefab;
    private GameObject temp1Obstacle, temp2Obstacle;
    private int level = 1, levelDiffuculty;
    [SerializeField] private int obstacleNumber;

    private float ObstacleHeight;

    [Tooltip("Space Between Obstacles")]
    [SerializeField] private float ObstacleSpace;

    [SerializeField] private float RotationSpeed;

    private Color _color;
    [SerializeField]
    private Material PlayerMat, PlaneMat;
    private void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
            level = PlayerPrefs.GetInt("Level");
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            level = PlayerPrefs.GetInt("Level");
        }

        Application.targetFrameRate = 60;


        obstacleNumber = 20 + level * 3;
        randomObstacleType();

        ObstacleHeight = GetComponent<Transform>().position.y - ObstacleSpace;

        for (int i = 0; i < obstacleNumber; i++)
        {
            temp1Obstacle = Instantiate(obstacleGameobjects[i]);
            temp1Obstacle.transform.position = new Vector3(0, ObstacleHeight, 0);
            temp1Obstacle.transform.eulerAngles = new Vector3(0, i * 6, 0); //This will give nice curve to obstacles
            temp1Obstacle.transform.SetParent(this.transform);
            ObstacleHeight -= ObstacleSpace;
        }

        temp2Obstacle = Instantiate(winPrefab, new Vector3(0, ObstacleHeight, 0), Quaternion.identity);
        temp1Obstacle.transform.SetParent(this.transform);
        temp2Obstacle.name = "WinPlane";

    }

    private void Update()
    {
        transform.Rotate(0, Time.deltaTime * RotationSpeed, 0, Space.Self);

        if (Input.GetMouseButtonDown(0))
        {
            _color = Random.ColorHSV(0, 1, 0.5f, 1, 1, 1);
            PlayerMat.color = _color;
            PlaneMat.color = _color;
        }
    }
    private void randomObstacleType()
    {
        int RandomedListItem = Random.Range(0, 5);
        obstacleGameobjects = new GameObject[obstacleNumber];
        SetLevelDifficulty();

        for (int i = 0; i < obstacleNumber; i++)
        {
            int RandomModel = Random.Range(levelDiffuculty, obstacleList[RandomedListItem].obstacleModels.Length);//models will be more difficult to pass with level progress

            obstacleGameobjects[i] = obstacleList[RandomedListItem].obstacleModels[RandomModel];
        }
    }
    private void SetLevelDifficulty()
    {
        if (level >= 0 && level < 5)
            levelDiffuculty = 0;
        else if (level >= 5 && level < 10)
            levelDiffuculty = 1;
        else if (level >= 10)
            levelDiffuculty = 2;
    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
