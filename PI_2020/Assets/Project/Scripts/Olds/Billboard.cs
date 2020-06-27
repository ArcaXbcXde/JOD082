using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    float timer = 0;
    Camera m_camera;
    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.25f)
        {
            transform.LookAt(m_camera.transform);
            timer = 0;
        }
    }
}
