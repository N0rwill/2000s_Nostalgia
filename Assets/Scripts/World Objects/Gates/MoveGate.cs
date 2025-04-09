using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGate : MonoBehaviour
{
    [SerializeField] private GameObject gate;

    public bool isOpen = false;

    void Start()
    {
        Vector3 newPosition = gate.transform.position;
        newPosition.y = 0.25f;
        gate.transform.position = newPosition;
    }

    public void Open()
    {
        Vector3 newPosition = gate.transform.position;
        newPosition.y = 0.7f;
        gate.transform.position = newPosition;
        isOpen = true;
    }
    
    public void Close()
    {
        Vector3 newPosition = gate.transform.position;
        newPosition.y = 0.25f;
        gate.transform.position = newPosition;
        isOpen = false;
    }
}
