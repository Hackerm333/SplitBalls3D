using MirkoZambito;
using UnityEngine;

public class StageController : MonoBehaviour
{

    [SerializeField] private MeshFilter meshFilter = null;
    [SerializeField] private MeshRenderer meshRenderer = null;
    [SerializeField] private MeshCollider meshCollider = null;


    public void OnSetup()
    {
        meshFilter.mesh = PoolManager.Instance.GetStageMesh();
        meshRenderer.material = PoolManager.Instance.GetStageMaterial();
        meshCollider.sharedMesh = PoolManager.Instance.GetStageMesh();

        //Setup Duplicator object
        foreach (SplitterController o in GetComponentsInChildren<SplitterController>())
        {
            o.OnSetup();
        }

        //Setup Locker object
        foreach (LockerController o in GetComponentsInChildren<LockerController>())
        {
            o.OnSetup();
        }
    }


    /// <summary>
    /// Setup material for blockers objects.
    /// </summary>
    /// <param name="material"></param>
    public void SetBlockerMaterial(Material material)
    {
        foreach (BlockerController o in GetComponentsInChildren<BlockerController>())
        {
            o.SetMaterial(material);
        }
    }


    /// <summary>
    /// Setup material for duplicator objects.
    /// </summary>
    /// <param name="material"></param>
    public void SetDuplicatorMaterial(Material material)
    {
        foreach (SplitterController o in GetComponentsInChildren<SplitterController>())
        {
            o.SetMaterial(material);
        }
    }


    /// <summary>
    /// Setup material for locker objects.
    /// </summary>
    /// <param name="material"></param>
    public void SetLockerMaterial(Material material)
    {
        foreach (LockerController o in GetComponentsInChildren<LockerController>())
        {
            o.SetMaterial(material);
        }
    }



    /// <summary>
    /// Create a ball for this stage.
    /// </summary>
    public void CreateBall()
    {
        BallController ballControl = PoolManager.Instance.GetBallControl(BallSize.BIG);
        ballControl.transform.position = transform.position + Vector3.up * (meshRenderer.bounds.size.y / 2f - 0.75f);
        ballControl.gameObject.SetActive(true);
        ballControl.SetHighIndex(Mathf.RoundToInt(ballControl.transform.position.y));
    }


    /// <summary>
    /// Get the size of this object.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetSize()
    {
        return meshRenderer.bounds.size;
    }


    /// <summary>
    /// Get the bottom position of this object. 
    /// </summary>
    /// <returns></returns>
    public Vector3 GetBottomPosition()
    {
        return transform.position + Vector3.down * (meshRenderer.bounds.size.y / 2f);
    }


    /// <summary>
    /// Disable this object.
    /// </summary>
    public void DisableObject()
    {
        //foreach (LockerController o in GetComponentsInChildren<LockerController>(true))
        //{
        //    o.gameObject.SetActive(true);
        //}
        gameObject.SetActive(false);
    }
}
