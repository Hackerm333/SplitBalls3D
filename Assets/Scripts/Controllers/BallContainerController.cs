using System.Collections;
using UnityEngine;

public class BallContainerController : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer = null;
    [SerializeField] private MeshCollider meshCollider = null;

    /// <summary>
    /// Move this object down and up.
    /// </summary>
    public void MoveDownAndUp()
    {
        StartCoroutine(CRMovingDownAndUp());
    }
    private IEnumerator CRMovingDownAndUp()
    {
        meshCollider.enabled = false;
        float movingTime = 0.5f;
        float t = 0;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y - 0.75f, startPos.z);
        while (t < movingTime)
        {
            t += Time.deltaTime;
            float factor = EasyType.MatchedLerpType(LerpType.EaseOutCubic, t / movingTime);
            transform.position = Vector3.Lerp(startPos, endPos, factor);
            yield return null;
        }

        startPos = endPos;
        endPos = new Vector3(startPos.x, startPos.y + 10f, startPos.z);
        t = 0;
        while (t < movingTime)
        {
            t += Time.deltaTime;
            float factor = EasyType.MatchedLerpType(LerpType.EaseOutCubic, t / movingTime);
            transform.position = Vector3.Lerp(startPos, endPos, factor);
            yield return null;
        }
        meshCollider.enabled = true;
        gameObject.SetActive(false);
    }



    /// <summary>
    /// Get the y size of this object.
    /// </summary>
    /// <returns></returns>
    public float GetSizeY()
    {
        return meshRenderer.bounds.size.y;
    }


    /// <summary>
    /// Get the current bottom position of this object.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetBottomPosition()
    {
        return transform.position + Vector3.down * meshRenderer.bounds.size.y;
    }



    /// <summary>
    /// Set the material for this object.
    /// </summary>
    /// <param name="material"></param>
    public void SetMaterial(Material material)
    {
        meshRenderer.material = material;
    }
}
