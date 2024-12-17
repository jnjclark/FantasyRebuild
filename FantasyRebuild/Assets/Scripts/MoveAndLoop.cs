using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndLoop : MonoBehaviour
{
    public float speed = 1f;
    public float resetPositionX;
    public float startPositionX;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= resetPositionX)
        {
            Vector2 newPosition = new Vector2(startPositionX, transform.position.y);
            transform.position = newPosition;
        }
    }
}

