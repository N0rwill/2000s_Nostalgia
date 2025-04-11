using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GateInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform gateTransform;
    [SerializeField] private float gateMoveDistance = 0.4f;
    [SerializeField] private float gateMoveSpeed = 2f;
    private Vector3 gateStartPosition;

    [SerializeField] private Transform leverTransform;
    [SerializeField] private float leverMoveDistance = -45f;
    [SerializeField] private float leverMoveSpeed = 2f;
    private Vector3 leverStartPosition;

    private bool isOpen = false;

    void Start()
    {
        gateStartPosition = gateTransform.localPosition;
        leverStartPosition = leverTransform.localPosition;
    }

    public bool canInteract()
    {
        return true;
    }

    public void Interact(Interactor interactor)
    {
        if (isOpen)
        {
            isOpen = false;
            StartCoroutine(MoveGate(gateTransform.transform.localPosition, gateStartPosition));

            StartCoroutine(MoveLever(leverTransform.localRotation.eulerAngles, leverStartPosition));
        }
        else
        {
            isOpen = true;
            StartCoroutine(MoveGate(gateTransform.transform.localPosition, gateStartPosition + Vector3.up * gateMoveDistance));

            StartCoroutine(MoveLever(leverTransform.localRotation.eulerAngles, new Vector3(leverStartPosition.x, leverStartPosition.y, leverStartPosition.z + leverMoveDistance)));
        }
    }

    private IEnumerator MoveGate(Vector3 start, Vector3 end)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * gateMoveSpeed;
            gateTransform.transform.localPosition = Vector3.Lerp(start, end, elapsedTime);
            yield return null;
        }
    }

    private IEnumerator MoveLever(Vector3 start, Vector3 end)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * leverMoveSpeed;
            leverTransform.localRotation = Quaternion.Lerp(Quaternion.Euler(start), Quaternion.Euler(end), elapsedTime);
            yield return null;
        }
    }
}
