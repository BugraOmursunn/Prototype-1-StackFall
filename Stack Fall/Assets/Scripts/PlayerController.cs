using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private bool _pressing;

    [SerializeField] private int DownVelocityValue;
    [SerializeField] private int JumpVelocityValue;

    [SerializeField] private bool IsImmortal;
    [SerializeField] private float AccumulatedTime;
    public GameObject fireShieldFx;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
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
                Debug.Log("Game Over");
            }
        }

    }
    private void OnCollisionStay(Collision other)
    {
        if (!_pressing)
        {
            Jump();
        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {

            }
            else if (other.gameObject.tag == "Plane")
            {

            }
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(0, JumpVelocityValue * Time.deltaTime, 0);
    }

}
