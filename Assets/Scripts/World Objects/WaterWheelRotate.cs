using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class WaterWheelRotate : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(-20 * Time.deltaTime, 0, 0);
    }
}
