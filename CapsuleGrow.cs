using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleGrow : MonoBehaviour
{
    [SerializeField] private float growthSpeed = 1.0f;

    private void Update()
    {
        float scaleChange = growthSpeed * Time.deltaTime;
        transform.localScale += new Vector3(scaleChange, scaleChange, scaleChange);
    }
}
