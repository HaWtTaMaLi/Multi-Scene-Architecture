using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteractable
{
    //
    public bool CanInteract() => canInteract;
    private float rotationAmount = -90;
    private float rotationSpeed = 1;
    private float forwardDirection = 0;
    public Coroutine AnimationCoroutine;
    private Vector3 startRotation;
    private Vector3 forward;

    [SerializeField] private bool canInteract = false;
    public bool isOpen = false;
    private bool isRotating = true;

    public void Awake()
    {
        startRotation = transform.rotation.eulerAngles;
        forward = transform.forward;
    }

    public IEnumerator DoRotationOpen(float forwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (forwardAmount >= forwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y - rotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y + rotationAmount, 0));
        }

        isOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * rotationSpeed;
        }
    }

    public IEnumerator DoRotationClose()
    {
        Quaternion StartRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation);

        isOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(StartRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * rotationSpeed;
        }
    }

    public void Open(Vector3 UserPosition)
    {
        if (!isOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (isRotating)
            {
                float dot = Vector3.Dot(forward, (UserPosition - transform.position).normalized);
                //Debug.Log($"Dot: {dot.ToString("N3")}");
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (isRotating)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
        }
    }

    public void ToggleDoor()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open(transform.position);
        }
    }
    public void Interact()
    {
        if (CanInteract())
        {
            ToggleDoor();
        }
        else
        {
            Debug.Log("Can't Interact");
        }
    }
    public void InRange()
    {
        canInteract = true;
    }
    public void OutofRange()
    {
        canInteract = false;
    }
}
