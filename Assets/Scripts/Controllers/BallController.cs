using MirkoZambito;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private BallSize ballSize = BallSize.BIG;
    [SerializeField] private Rigidbody rigid = null;
    [SerializeField] private MeshRenderer meshRenderer = null;

    [SerializeField] private int highIndex = 0;

    public BallSize BallSize { get { return ballSize; } }
    public int HighIndex { private set; get; }



    private void FixedUpdate()
    {
        if (IngameManager.Instance.ForceDir != 0)
        {
            Vector3 forceDir = (IngameManager.Instance.ForceDir == 1) ? transform.right : -transform.right;
            forceDir *= IngameManager.Instance.ForceTurn;
            rigid.AddForceAtPosition(forceDir * 0.50f, transform.position, ForceMode.Impulse);
        }
    }




    /// <summary>
    /// Get half of this object's size. 
    /// </summary>
    /// <returns></returns>
    public Vector3 GetHalfSize()
    {
        return meshRenderer.bounds.size / 2f;
    }



    /// <summary>
    /// Set the high index for this ball.
    /// </summary>
    /// <param name="index"></param>
    public void SetHighIndex(int index)
    {
        HighIndex = index;
        highIndex = index;
    }



    /// <summary>
    /// Disable this object.
    /// </summary>
    public void DisableObject()
    {
        rigid.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
