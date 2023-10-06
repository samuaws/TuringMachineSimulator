using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Range(0,5)]
    public float speed = 1;
    float horizantal;


    void Update()
    {
        horizantal = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        transform.Translate(horizantal * speed, 0, 0);
    }
}
