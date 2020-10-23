using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCamera : MonoBehaviour
{
    [SerializeField]
    GameObject targetObject;

    [SerializeField]
    float moveSpeed = .5f;

    [SerializeField]
    float rotateSpeed = .5f;

    Vector3 m_offsetToTarget;

    Quaternion m_lookDirection;

    void OnEnable()
    {
        m_offsetToTarget = targetObject.transform.InverseTransformPoint(transform.position);
        m_lookDirection = Quaternion.Inverse(targetObject.transform.rotation) * transform.rotation;
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = targetObject.transform.TransformPoint(m_offsetToTarget);
        transform.position = transform.position + (targetPosition - transform.position) * Time.fixedDeltaTime * moveSpeed;

        Quaternion targetRotation = targetObject.transform.rotation * m_lookDirection;
        float angle = Quaternion.Angle(targetRotation, transform.rotation);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angle * rotateSpeed * Time.fixedDeltaTime);
    }
}
