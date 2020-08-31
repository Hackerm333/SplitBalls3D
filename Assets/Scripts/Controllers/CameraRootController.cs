using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRootController : MonoBehaviour
{

    private Vector3 inputPos = Vector3.zero;

    private int turn = 1;
    private void Update()
    {
        if (IngameManager.Instance.IngameState == MirkoZambito.IngameState.Ingame_Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                inputPos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 currentPos = Input.mousePosition;
                Quaternion newQuaternion = transform.rotation;
                newQuaternion.eulerAngles -= Vector3.up * (inputPos.x - currentPos.x);
                transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion, IngameManager.Instance.RotatingSpeed * Time.deltaTime);
                IngameManager.Instance.SetForceDir((inputPos.x < currentPos.x) ? 1 : -1);
                inputPos = currentPos;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                IngameManager.Instance.SetForceDir(0);
            }
        }
    }


    public void ResetRotationandMoveDown()
    {
        StartCoroutine(CRMovingDown());
        StartCoroutine(CRRotating());
    }
    private IEnumerator CRMovingDown()
    {
        float resetTime = 1f;
        float t = 0;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.down * 21f;
        while (t < resetTime)
        {
            t += Time.deltaTime;
            float factor = t / resetTime;
            transform.position = Vector3.Lerp(startPos, endPos, factor);
            yield return null;
        }
    }

    private IEnumerator CRRotating()
    {
        turn *= -1;
        float resetTime = 1f;
        float t = 0;
        Quaternion startQua = transform.rotation;
        Quaternion endQua = Quaternion.Euler(0, (turn < 0) ? 180f : 360f, 0);
        while (t < resetTime)
        {
            t += Time.deltaTime;
            float factor = t / resetTime;
            transform.rotation = Quaternion.Lerp(startQua, endQua, factor);
            yield return null;
        }
    }




    /// <summary>
    /// Reset the rotation (facing toward to camera).
    /// </summary>
    public void ResetRotationToDefault()
    {
        StartCoroutine(CRRotatingToDefault());
    }
    private IEnumerator CRRotatingToDefault()
    {
        float resetTime = 0.5f;
        float t = 0;
        Quaternion startQua = transform.rotation;
        Quaternion endQua = Quaternion.Euler(0, (turn < 0) ? 180f : 360f, 0);
        while (t < resetTime)
        {
            t += Time.deltaTime;
            float factor = t / resetTime;
            transform.rotation = Quaternion.Lerp(startQua, endQua, factor);
            yield return null;
        }
    }
}
