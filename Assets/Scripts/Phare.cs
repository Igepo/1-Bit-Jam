using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phare : MonoBehaviour
{
    public float speed = 25f;
    public GameObject lighthouseLight;

    void Update()
    {
        lighthouseLight.transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
