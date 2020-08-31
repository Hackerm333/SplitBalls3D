using System.Collections;
using MirkoZambito;
using UnityEngine;

public class SplitterController : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private int duplicateTime = 2;
    [SerializeField] private BallSize ballSizeToCreate = BallSize.BIG;

    [Header("References")]
    [SerializeField] private MeshRenderer meshRenderer = null;


    public int HighIndex { private set; get; }


    private SplitterTextController splitterTextControl = null;
    private int totalCreatedBall = 0;


    public void OnSetup()
    {
        HighIndex = Mathf.RoundToInt(transform.position.y);
        Vector3 textPos = transform.localPosition + transform.InverseTransformDirection(transform.forward) * (meshRenderer.bounds.size.z / 2.05f);
        textPos += Vector3.up * (meshRenderer.bounds.size.y / 2f);
        textPos.y += 0.65f;
        splitterTextControl = PoolManager.Instance.GetDuplicatorTextControl();
        splitterTextControl.transform.SetParent(transform.parent);
        splitterTextControl.transform.localEulerAngles = Vector3.zero;
        splitterTextControl.transform.localPosition = textPos;
        splitterTextControl.gameObject.SetActive(true);
        splitterTextControl.OnSetup(duplicateTime);
        StartCoroutine(CRCreatingBalls());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            BallController ballControl = other.gameObject.GetComponent<BallController>();
            if (ballControl.HighIndex > HighIndex)
            {
                ballControl.DisableObject();
                totalCreatedBall += duplicateTime;
            }
        }
    }


    private IEnumerator CRCreatingBalls()
    {
        while (true)
        {
            while (totalCreatedBall > 0)
            {
                BallController ballControl = PoolManager.Instance.GetBallControl(ballSizeToCreate);
                Vector3 ballPos = transform.position + Vector3.down * (meshRenderer.bounds.size.y / 2f);
                ballPos += Vector3.down * ballControl.GetHalfSize().y;
                float devitation = meshRenderer.bounds.size.x / 4f;
                ballPos.x += Random.Range(-devitation, devitation);
                ballControl.transform.position = ballPos;
                ballControl.SetHighIndex(HighIndex);
                ballControl.gameObject.SetActive(true);
                splitterTextControl.ScaleBounce();
                yield return new WaitForSeconds(0.05f);
                totalCreatedBall--;
                ServicesManager.Instance.SoundManager.PlayOneSound(ServicesManager.Instance.SoundManager.duplicatingBall);
            }

            yield return null;
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
}
