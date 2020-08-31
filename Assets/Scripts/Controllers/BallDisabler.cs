using UnityEngine;

public class BallDisabler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            collision.collider.GetComponent<BallController>().DisableObject();
            IngameManager.Instance.CheckOutOfBall();
        }
    }
}
