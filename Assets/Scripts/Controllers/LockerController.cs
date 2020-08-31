using UnityEngine;
using System.Collections;
using MirkoZambito;

public class LockerController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private int ballNumberToUnlock = 10;

    [Header("References")]
    [SerializeField] private MeshRenderer meshRenderer = null;

    private LockerTextController lockerTextControl = null;
    private GameObject currentBall = null;
    private int ballsLeft = 0;
    private bool isFlashing = false;

    public void OnSetup()
    {
        ballsLeft = ballNumberToUnlock;
        Vector3 textPos = transform.localPosition + transform.InverseTransformDirection(transform.forward) * (meshRenderer.bounds.size.z / 2.05f);
        textPos += Vector3.up * (meshRenderer.bounds.size.y / 2f);
        textPos.y += 0.65f;
        lockerTextControl = PoolManager.Instance.GetLockerTextControl();
        lockerTextControl.transform.SetParent(transform.parent);
        lockerTextControl.transform.localEulerAngles = Vector3.zero;
        lockerTextControl.transform.localPosition = textPos;
        lockerTextControl.gameObject.SetActive(true);
        lockerTextControl.OnUpdateText(ballsLeft);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && other.gameObject != currentBall)
        {
            other.gameObject.GetComponent<BallController>().DisableObject();
            ballsLeft--;
            lockerTextControl.OnUpdateText(ballsLeft);
            if (!isFlashing)
            {
                ServicesManager.Instance.SoundManager.PlayOneSound(ServicesManager.Instance.SoundManager.lockerCount);
                isFlashing = true;
                StartCoroutine(CRFlashing());
            }
            if (ballsLeft <= 0)
            {
                ServicesManager.Instance.SoundManager.PlayOneSound(ServicesManager.Instance.SoundManager.breakTheLocker);
                lockerTextControl.gameObject.SetActive(false);
                gameObject.SetActive(false);
                EffectManager.Instance.PlayDisableLockerEffect(transform.position, meshRenderer.bounds.size);
            }
        }
    }


    /// <summary>
    /// Set the material for this object.
    /// </summary>
    /// <param name="mat"></param>
    public void SetMaterial(Material mat)
    {
        meshRenderer.material = mat;
    }


    private IEnumerator CRFlashing()
    {
        Color currentColor = meshRenderer.material.color;
        meshRenderer.material.color = Color.white;
        yield return null;
        meshRenderer.material.color = currentColor;
        isFlashing = false;
    }
}
