using UnityEngine;

public class EnvironmentViewController : MonoBehaviour
{
    [SerializeField] private RectTransform scrollViewTrans = null;
    public void OnShow()
    {
        ViewManager.Instance.ScaleRect(scrollViewTrans, Vector2.zero, Vector2.one, 0.75f);
    }

    private void OnDisable()
    {
        scrollViewTrans.localScale = Vector2.zero;
    }

    public void CloseBtn()
    {
        ViewManager.Instance.PlayClickButtonSound();
        ViewManager.Instance.HomeViewController.OnSubViewClose();
        gameObject.SetActive(false);
    }
}
