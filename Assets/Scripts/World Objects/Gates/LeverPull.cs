using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPull : MonoBehaviour
{
    [SerializeField] private GameObject lever;

    void open()
    {
        lever.transform.rotation = Quaternion.Euler(0, 80, 0);
    }

    void close()
    {
        lever.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
