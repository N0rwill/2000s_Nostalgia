using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField] private bool IsRotatingDoor = true;
    [SerializeField] private float speed = 1f;
    
    [Header("Rotation Configs")]
    [SerializeField] private float RotationAmount = 90f;
    public GameObject gate;

    private Vector3 StartRotation;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;
    // Start is called before the first frame update
    void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        //Because "forward" is pointing toward the door frame technically
        Forward = gate.transform.right;
    }
    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen) 
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor) 
            {
                float dot = Vector3.Dot(Forward, (UserPosition - gate.transform.position).normalized);
                Debug.Log($"Dot: {dot.ToString("N3")}");
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount) 
    {
        Quaternion startRotation = gate.transform.rotation;
        Quaternion endRotation;
        
        endRotation = Quaternion.Euler(new Vector3(0, StartRotation.y - RotationAmount, 0));
  
        IsOpen = true;
        float time = 0;
        while (time < 1) 
        {
            gate.transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    public void Close() 
    {
        if (IsOpen) 
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }
            if (IsRotatingDoor) 
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }

    private IEnumerator DoRotationClose() 
    {
        Quaternion startRotation = gate.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            gate.transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}