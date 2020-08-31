using UnityEngine.SceneManagement;
using UnityEngine;
using MirkoZambito;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HomeManager : MonoBehaviour
{

    [SerializeField] private Rigidbody ballRigidPrefab = null;
    [SerializeField] private MeshFilter stageMeshFilter = null;
    [SerializeField] private MeshRenderer stageMeshRenderer = null;
    [SerializeField] private List<StageSelectionData> listStageSelectionData = new List<StageSelectionData>();

    private List<Rigidbody> listBallRigid = new List<Rigidbody>();
    private void Start()
    {
        Application.targetFrameRate = 60;
        ViewManager.Instance.OnLoadingSceneDone(SceneManager.GetActiveScene().name);

        if (IngameManager.SelectedStageType == 0)
        {
            IngameManager.SetStageType(listStageSelectionData[0].StageType);
        }

        //Setup stage
        foreach(StageSelectionData o in listStageSelectionData)
        {
            if (o.StageType == IngameManager.SelectedStageType)
            {
                stageMeshFilter.mesh = o.StageMesh;
                stageMeshRenderer.material = o.StageMaterial;
                break;
            }
        }

        StartCoroutine(CRCreatingBall());
    }



    private IEnumerator CRCreatingBall()
    {
        while (true)
        {
            Rigidbody ballRigid = GetBallRigid();
            ballRigid.transform.position = new Vector3(0, 6.3f, 0);
            ballRigid.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(CRMovingBall(ballRigid));
            yield return new WaitForSeconds(1);
        }
    }


    private IEnumerator CRMovingBall(Rigidbody rigidbody)
    {
        float t = 0.5f;
        while (true)
        {
            t = 1f;
            while (t > 0)
            {
                t -= Time.deltaTime;
                rigidbody.AddForceAtPosition(Vector3.right * 0.15f, transform.position, ForceMode.Impulse);
                rigidbody.AddTorque(-rigidbody.transform.forward * 0.05f, ForceMode.Force);
                yield return null;
            }

            t = 1f;
            while (t > 0)
            {
                t -= Time.deltaTime;
                rigidbody.AddForceAtPosition(Vector3.left * 0.15f, transform.position, ForceMode.Impulse);
                rigidbody.AddTorque(-rigidbody.transform.forward * 0.15f, ForceMode.Force);
                yield return null;
            }


            Vector2 viewport = Camera.main.WorldToViewportPoint(rigidbody.transform.position);
            if (viewport.y <= -0.1f)
            {
                rigidbody.Sleep();
                rigidbody.gameObject.SetActive(false);
                yield break;
            }
        }
    }


    private Rigidbody GetBallRigid()
    {
        Rigidbody rigidbody = listBallRigid.Where(a => !a.gameObject.activeInHierarchy).FirstOrDefault();

        if (rigidbody == null)
        {
            rigidbody = Instantiate(ballRigidPrefab, Vector3.zero, Quaternion.identity);
            listBallRigid.Add(rigidbody);
            rigidbody.gameObject.SetActive(false);
        }

        return rigidbody;
    }

}
