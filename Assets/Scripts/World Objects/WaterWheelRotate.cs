using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class WaterWheelRotate : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Rotate(-1, 0, 0);
    }
}
