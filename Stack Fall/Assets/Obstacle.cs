using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshrenderer;
    private Collider _collider;
    private ObstacleController _obstacleController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshrenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _obstacleController = transform.parent.GetComponent<ObstacleController>();
    }
    public void Shatter()
    {
        _rigidbody.isKinematic = false;
        _collider.enabled = false;

        Vector3 forcePoint = transform.parent.position;
        float parentXpos = transform.parent.position.x;
        float CenterXpos = _meshrenderer.bounds.center.x;

        Vector3 subdir = (parentXpos - CenterXpos < 0) ? Vector3.right : Vector3.left;
        Vector3 dir = (Vector3.up * 1.5f + subdir).normalized;

        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        _rigidbody.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        //_rigidbody.AddTorque(Vector3.left * torque);
        //_rigidbody.velocity = Vector3.down;
    }
}
