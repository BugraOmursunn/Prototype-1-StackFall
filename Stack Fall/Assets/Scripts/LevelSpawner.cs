using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class LevelSpawner : MonoBehaviour
{
    [Serializable]
    public class obstacleTypes
    {
        public string name;
        public GameObject[] obstacleModels;
    }
    public List<obstacleTypes> obstacleList;

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
    private bool IsRotating;

    private void Start()
    {
        Application.targetFrameRate = 60;
        randomObstacleType();

        obstacleNumber = 20 + level * 3;//default

        ObstacleHeight = GetComponent<Transform>().position.y - ObstacleSpace;

        for (int i = 0; i < obstacleNumber; i++)
        {
            temp1Obstacle = Instantiate(obstacleGameobjects[i]);
            temp1Obstacle.transform.position = new Vector3(0, ObstacleHeight, 0);
            temp1Obstacle.transform.eulerAngles = new Vector3(0, i * 8, 0); //This will give nice curve to obstacles
            temp1Obstacle.transform.SetParent(this.transform);

            ObstacleHeight -= ObstacleSpace;
        }

        temp2Obstacle = Instantiate(winPrefab, new Vector3(0, ObstacleHeight, 0), Quaternion.identity);
        temp1Obstacle.transform.SetParent(this.transform);
        temp2Obstacle.name = "WinPlane";
        IsRotating = true;
    }

    private void Update()
    {
        if (IsRotating)
        {
            transform.Rotate(0, Time.deltaTime * RotationSpeed, 0, Space.Self);
        }
    }
    private void randomObstacleType()
    {
        int RandomedListItem = Random.Range(0, 5);
        obstacleGameobjects = new GameObject[obstacleNumber];

        for (int i = 0; i < obstacleNumber; i++)
        {
            SetLevelDifficulty();
            int RandomModel = Random.Range(levelDiffuculty, obstacleList[RandomedListItem].obstacleModels.Length);//models will be more difficult to pass with level progress

            obstacleGameobjects[i] = obstacleList[RandomedListItem].obstacleModels[RandomModel];
        }
    }
    private void SetLevelDifficulty()
    {
        if (level < 5)
            levelDiffuculty = 0;
        else if (level < 10)
            levelDiffuculty = 1;
        else if (level < 15)
            levelDiffuculty = 2;
        else if (level < 20)
            levelDiffuculty = 3;

    }

}
