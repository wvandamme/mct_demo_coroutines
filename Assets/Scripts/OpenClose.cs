using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{

    public Collider m_collider;
    public float m_angle = 100.0f;
    public float m_time = 2.0f;

    private bool m_moving = false;
    private bool m_is_open = false;

    void Update()
    {
        if (!m_moving && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
            {
               if (hit.collider == m_collider)
               {
                    m_moving = true;
                    if (m_is_open)
                    {
                        StartCoroutine(CloseDoor());
                    }
                    else
                    { 
                        StartCoroutine(OpenDoor());
                    }
               }
            }
        }
    }

    IEnumerator OpenDoor()
    {
        Debug.Log("OpenDoor");
        Vector3 start = transform.localEulerAngles;
        Vector3 end = start + new Vector3(0.0f, m_angle, 0.0f);
        float t = 0;
        while (t < m_time)
        {
           t += Time.deltaTime;
           transform.localEulerAngles = Vector3.Lerp(start, end, t / m_time);
           yield return null;
        }       
        m_is_open = true;
        m_moving = false;
    }

    IEnumerator CloseDoor()
    {
        Debug.Log("CloseDoor");
        Vector3 start = transform.localEulerAngles;
        Vector3 end = start - new Vector3(0.0f, m_angle, 0.0f);
        float t = 0;
        while (t < m_time)
        {
            t += Time.deltaTime;
            transform.localEulerAngles = Vector3.Lerp(start, end, t / m_time);
            yield return null;
        }
        m_is_open = false;
        m_moving = false;
    }

}

