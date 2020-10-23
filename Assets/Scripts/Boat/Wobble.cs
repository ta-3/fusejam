using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Wobble : MonoBehaviour
{
    [SerializeField]
    GameObject m_targetObject;

    [SerializeField]
    [Range(0f, 1f)]
    float m_power = .5f;

    GameObject m_object;
    void OnEnable()
    {
        if (m_targetObject == null)
            m_object = gameObject;
        else
            m_object = m_targetObject;
    }

    void FixedUpdate()
    {
        Rigidbody targetBody = m_object.GetComponent<Rigidbody>();
        Transform targetTransform = m_object.transform;

        float pitch = targetTransform.localEulerAngles.x;

        if (pitch > 180f)
            pitch = -(360f - pitch);

        float roll = targetTransform.localEulerAngles.z;

        if (roll > 180f)
            roll = -(360f - roll);

        targetBody.AddTorque(-targetTransform.forward * roll * m_power);
        targetBody.AddTorque(-targetTransform.right * pitch * m_power);
    }
}
