using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectManager : MonoBehaviour {

    public static EffectManager Instance { private set; get; }

    [SerializeField] private ParticleSystem disableLockerEffectPrefab = null;

    private List<ParticleSystem> listDisableLockerEffect = new List<ParticleSystem>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(Instance.gameObject);
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }


    /// <summary>
    /// Play the given particle then disable it 
    /// </summary>
    /// <param name="par"></param>
    /// <returns></returns>
    private IEnumerator CRPlayParticle(ParticleSystem par)
    {
        par.Play();
        yield return new WaitForSeconds(2f);
        par.gameObject.SetActive(false);
    }


    /// <summary>
    /// Play a disable locker effect at given position.
    /// </summary>
    /// <param name="pos"></param>
    public void PlayDisableLockerEffect(Vector3 pos, Vector3 size)
    {
        //Find in the list
        ParticleSystem disableLocker = listDisableLockerEffect.Where(a => !a.gameObject.activeInHierarchy).FirstOrDefault();

        if (disableLocker == null)
        {
            //Didn't find one -> create new one
            disableLocker = Instantiate(disableLockerEffectPrefab, pos, Quaternion.identity);
            disableLocker.gameObject.SetActive(false);
            listDisableLockerEffect.Add(disableLocker);
        }

        disableLocker.transform.position = pos;
        var shape = disableLocker.shape;
        shape.scale = size;
        disableLocker.gameObject.SetActive(true);
        StartCoroutine(CRPlayParticle(disableLocker));
    }
}
