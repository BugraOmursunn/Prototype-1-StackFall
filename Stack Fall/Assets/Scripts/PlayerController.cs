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
    public GameObject fireShieldFx;


    public enum PlayerState
    {
        MainMenu,
        Playing,
        Died,
        EndMenu
    }
    [HideInInspector]
    public PlayerState _playerState = PlayerState.MainMenu;

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
                other.transform.parent.GetComponent<ObstacleController>().PlayShatterAnim();
            }
        }
        else
        {
            if (other.gameObject.tag == "enemy")
            {
                other.transform.parent.GetComponent<ObstacleController>().PlayShatterAnim();
            }
            else if (other.gameObject.tag == "plane")
            {
                _pressing = false;
                _playerState = PlayerState.Died;
                FindObjectOfType<UIController>().SetUIState(2);
            }
        }
        if (other.gameObject.tag == "Finish")
        {
            _pressing = false;
            _playerState = PlayerState.EndMenu;
            FindObjectOfType<UIController>().SetUIState(3);
        }

    }
    private void OnCollisionStay(Collision other)
    {
        if (!_pressing)
        {
            Jump();
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(0, JumpVelocityValue * Time.deltaTime, 0);
    }

}
