using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private void Update()
    {
        var nextPosition = transform.position;
        nextPosition.x += 0.001f;
        transform.position = nextPosition;
    }
}
