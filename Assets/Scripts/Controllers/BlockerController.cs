using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerController : MonoBehaviour
{
    private MeshRenderer meshRenderer = null;



    /// <summary>
    /// Set the material for this object.
    /// </summary>
    /// <param name="mat"></param>
    public void SetMaterial(Material mat)
    {
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = mat;
    }
}
