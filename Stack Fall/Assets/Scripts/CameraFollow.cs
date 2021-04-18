using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float CameraPositionY;
    private Transform Player, WinPlane;
    [SerializeField] private float CameraOffset;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (WinPlane == null)
        {
            WinPlane = GameObject.Find("WinPlane").transform;
        }
        if (transform.position.y > Player.position.y && transform.position.y > WinPlane.position.y + CameraOffset)
        {
            transform.position = new Vector3(transform.position.x, Player.position.y, transform.position.z);
        }
    }
}