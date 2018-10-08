using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public JoyconManager Joycons;
    CharacterManager character;

    // Lissage de la transition
    public float smooth = 20.0f;
    public float tiltAngle = 1.0f;
    public float fovX = 30.0f;
    public float fovY = 60.0f;

    // Position ciblée
    private Quaternion m_target;
    private Vector3 m_oldVect = new Vector3(0, 0, 0);
    private bool m_outOfFov = false;
    private bool m_holdingView = false;

    void Start(){
        character=GetComponent<CharacterManager>();
    }

    public Quaternion GetVector(Vector3 i_b, Vector3 j_b, Vector3 k_b)
    {
        Vector3 v1 = new Vector3(j_b.x, i_b.x, k_b.x);
        Vector3 v2 = -(new Vector3(j_b.z, i_b.z, k_b.z));
        Quaternion res = Quaternion.identity;
        if (v2 != Vector3.zero)
        {
            res = Quaternion.LookRotation(v1, v2);
        }
        var rot = Quaternion.AngleAxis(90.0f, new Vector3(1.0f, 0.0f, 0.0f));
        return rot * res;
    }

    void RotateTo(Vector3 position)
    {
        position.z = 0.0f;
        if ((fovX < position.x && position.x < 360.0f - fovX) || (fovY < position.y && position.y < 360.0f - fovY))
        {
            m_outOfFov = true;
            if (fovX < position.x && position.x < 360.0f - fovX)
            {
                if (position.x - m_oldVect.x >= float.Epsilon)
                {
                    position.x = fovX;
                }
                else
                {
                    position.x = 360.0f - fovX;
                }
            }
            if (fovY < position.y && position.y < 360.0f - fovY)
            {
                if (position.y - m_oldVect.y >= float.Epsilon)
                {
                    position.y = fovY;
                }
                else
                {
                    position.y = 360.0f - fovY;
                }
            }
            var Target = Quaternion.Euler(position);
            transform.rotation = Quaternion.Slerp(transform.rotation, Target, Time.deltaTime * smooth);
        }
        else
        {
            m_oldVect = position;
            m_outOfFov = false;
        }

        if (!m_outOfFov)
        {
            m_target = Quaternion.Euler(position);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_target, Time.deltaTime * smooth);
        }
    }

    void Update()
    {
        if (Joycons.j[1].GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            m_holdingView = true;
            character.EnableMovement(false);
        }
        if (Joycons.j[1].GetButtonUp(Joycon.Button.SHOULDER_1))
        {
            m_holdingView = false;
             character.EnableMovement(true);
           // character.RecalibrateJoycons();
           character.RecalibrateHands();
        }

        if (m_holdingView)
        {
            var position = GetVector(
                Joycons.j[1].i_b,
                Joycons.j[1].j_b,
                Joycons.j[1].k_b
            ).eulerAngles;
            RotateTo(position);
        }
    }
}
