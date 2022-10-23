using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follow : MonoBehaviour
{
    public float speed = 10;
    public PathCreator ph;
    public float distance = 0;
    public Vector2[] points;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance+=Time.fixedDeltaTime*speed;
        transform.position = ph.path.GetPointAtDistance(distance);
        Debug.Log(ph.path.GetPoint(1));
    }
}
