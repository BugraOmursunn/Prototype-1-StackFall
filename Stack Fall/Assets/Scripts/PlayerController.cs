using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private bool _pressing;
    private bool IsAlive;
    [SerializeField] private int DownVelocityValue;
    [SerializeField] private int JumpVelocityValue;

    [SerializeField] private bool IsImmortal;
    [SerializeField] private float AccumulatedTime;
    public GameObject fireShieldFx, DeathFx, WinFx;
    private int BreakedObstacles;

    public enum PlayerState
    {
        MainMenu,
        Playing,
        Died,
        EndMenu
    }
    [HideInInspector]
    public PlayerState _playerState = PlayerState.MainMenu;

    [SerializeField]
    AudioClip win, death, idestroy, destroy, bounce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        switch (_playerState)
        {
            case PlayerState.MainMenu:
                if (Input.GetMouseButtonDown(0))
                {
                    _pressing = true;
                    _playerState = PlayerState.Playing;
                    FindObjectOfType<UIController>().SetUIState(1);
                }
                break;
            case PlayerState.Playing:
                if (Input.GetMouseButtonDown(0))
                    _pressing = true;
                if (Input.GetMouseButtonUp(0))
                    _pressing = false;

                if (_pressing)
                    AccumulatedTime += Time.deltaTime;
                else if (AccumulatedTime > 0)
                    AccumulatedTime -= Time.deltaTime;


                if (AccumulatedTime >= 0.5f)
                {
                    fireShieldFx.SetActive(true);
                    IsImmortal = true;
                }
                else if (AccumulatedTime <= 0f)
                {
                    fireShieldFx.SetActive(false);
                    IsImmortal = false;
                }
                break;

            case PlayerState.Died:
                if (Input.GetMouseButtonDown(0))
                {
                    FindObjectOfType<LevelSpawner>().RestartLevel();
                    FindObjectOfType<UIController>().SetUIState(1);
                }
                break;
            case PlayerState.EndMenu:
                if (Input.GetMouseButtonDown(0))
                {
                    FindObjectOfType<LevelSpawner>().NextLevel();
                }
                break;
        }
    }
    private void FixedUpdate()
    {
        if (_pressing)
        {
            rb.velocity = new Vector3(0, -DownVelocityValue * Time.fixedDeltaTime, 0);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!_pressing)
        {
            Jump();
        }
        else if (IsImmortal == true)
        {
            if (other.gameObject.tag != "Finish")//if game is finished
            {
                BreakObstacle(other, 0);
            }
        }
        else
        {
            if (other.gameObject.tag == "enemy")
            {
                BreakObstacle(other, 1);
            }
            else if (other.gameObject.tag == "plane")
            {
                _pressing = false;
                _playerState = PlayerState.Died;
                FindObjectOfType<UIController>().SetUIState(2);
                ScoreManager.instance.ResetScore();//reset score
                SoundManager.instance.playSoundFx(death, 1);
                DeathFx.SetActive(true);
            }
        }

        if (other.gameObject.tag == "Finish" && _playerState == PlayerState.Playing)
        {
            _pressing = false;
            _playerState = PlayerState.EndMenu;
            FindObjectOfType<UIController>().SetUIState(3);
            SoundManager.instance.playSoundFx(win, 1);
            WinFx.SetActive(true);
        }

    }
    private void OnCollisionStay(Collision other)
    {
        if (!_pressing)
        {
            Jump();
            SoundManager.instance.playSoundFx(bounce, 1);
        }
    }
    private void BreakObstacle(Collision other, int BreakType)
    {
        other.transform.parent.GetComponent<ObstacleController>().PlayShatterAnim();
        BreakedObstacles++;

        int TotalObstacleNumber = 20 + PlayerPrefs.GetInt("Level") * 3;

        FindObjectOfType<UIController>().ProgressLevelBar(BreakedObstacles / (float)TotalObstacleNumber);

        switch (BreakType)
        {
            case 0://break while immortal
                ScoreManager.instance.AddScore(1);//1 point
                SoundManager.instance.playSoundFx(idestroy, 1);
                break;
            case 1://break normal
                ScoreManager.instance.AddScore(2);//2 point
                SoundManager.instance.playSoundFx(destroy, 1);
                break;
        }

    }
    private void Jump()
    {
        rb.velocity = new Vector3(0, JumpVelocityValue * Time.deltaTime, 0);
    }

}
