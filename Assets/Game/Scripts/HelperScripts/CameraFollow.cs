using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]private Transform Target;
    [SerializeField]private float sSpeed = 10f;
    [SerializeField] private Vector3 dist;

    Vector3 dPos;
    Vector3 sPos;

    private void LateUpdate()
    {
        dPos = Target.position + dist;
        sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);
        transform.position = sPos;
        transform.LookAt(Target.position);
    }
}
