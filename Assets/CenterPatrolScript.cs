using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CenterPatrolScript : MonoBehaviour
{

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    Vector2 targetPosition;

    public float minSpeed;
    public float maxSpeed;

    float speed;

    public float secondsToMaxDifficulty;

    public GameObject mercury;

    void Start()
    {
        targetPosition = new Vector2(0, 0);
    }

    void Update()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPercent());
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            //transform.RotateAround(new Vector3(0,0,-10), new Vector3(0,0,1), Time.deltaTime * 12); //point, axis, speed
            transform.RotateAround(mercury.transform.position, new Vector3(0, 0, 1), Time.deltaTime * 12);
        }
        else
        {
            targetPosition = new Vector2(0, 0);
        }
    }

    float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}