using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.2f;

    public Transform m_target;

    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;
    public float m_RotateSpeed = 1f;
    public float m_zoomSpeed = 10f;
    public float m_YSpeed = 10;
    public float m_XSpeed = 10f;

    private void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        m_DesiredPosition = m_target.position;
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 cameraPos = Camera.main.transform.localPosition;
        cameraPos.z += scroll * m_zoomSpeed;
        cameraPos.z = Mathf.Clamp(cameraPos.z, -30, -5);
        Camera.main.transform.localPosition = cameraPos;

        if (Input.GetMouseButton(1))
        {
            Vector3 angles = transform.localEulerAngles
                + Vector3.right * Input.GetAxis("Mouse Y")
                + Vector3.up * Input.GetAxis("Mouse X");

            float Y = Input.GetAxis("Mouse Y");
            float X = Input.GetAxis("Mouse X");

            angles.x += Y * m_YSpeed;
            angles.y += X * m_XSpeed;
            angles.x = Mathf.Clamp(angles.x, 10, 80);

            transform.localEulerAngles = angles;
        }
    }
}
