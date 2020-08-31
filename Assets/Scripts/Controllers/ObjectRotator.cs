using UnityEngine;
using MirkoZambito;

public class ObjectRotator : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] RotateDirection rotateDirection = RotateDirection.LEFT;
    [SerializeField] private float rotatingSpeed = 50f;

    private void Update()
    {
        transform.eulerAngles += (rotateDirection == RotateDirection.LEFT) ? Vector3.forward : Vector3.back * rotatingSpeed * Time.deltaTime;
    }
}
