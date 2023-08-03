using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public static CameraFollower instance;
    [SerializeField] private int  dampValue;
    [SerializeField] private Vector3 offset;
    [SerializeField] float xvalue;

    private Transform target;

    void Update()
    {
        if (!target) return;
        Move();
    }
    internal void AssignTarget(Transform _target)
    {
        target = _target;
    }
    private void Move()
    {
        Vector3 targetPos = new Vector3(xvalue, target.position.y + offset.y, target.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, dampValue * Time.deltaTime);
    }

}
