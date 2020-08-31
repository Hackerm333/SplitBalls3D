using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MirkoZambito;

[RequireComponent(typeof(ScrollRect))]
public class ScrollviewSnap : MonoBehaviour, IEndDragHandler
{
    [Header("Snapping Config")]
    public float snapTime = 0.3f;

    ScrollRect scrollRect;
    RectTransform content;
    GridLayoutGroup contentGridLayoutGroup;
    IEnumerator snapCoroutine;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = scrollRect.content;
        contentGridLayoutGroup = content.GetComponent<GridLayoutGroup>();

        // Disable inertia so that it won't affect our snapping 
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        scrollRect.inertia = false;

        float x = (contentGridLayoutGroup.cellSize.x * (content.transform.childCount - 1)) + (contentGridLayoutGroup.spacing.x * (content.transform.childCount - 1));
        content.anchoredPosition = Vector2.zero;
        content.sizeDelta = new Vector2(x, 0);
    }

    // Called when the user starts dragging the element this script is attached to..
    public void OnBeginDrag(PointerEventData data)
    {
        // Stop snapping if any
        if (snapCoroutine != null)
        {
            StopCoroutine(snapCoroutine);
            snapCoroutine = null;
        }
    }

    // Called when the user stops dragging this UI Element.
    public void OnEndDrag(PointerEventData data)
    {
        float currentPosX = -content.localPosition.x;
        float itemWidth = contentGridLayoutGroup.cellSize.x + contentGridLayoutGroup.spacing.x;
        int index = Mathf.RoundToInt(currentPosX / itemWidth);
        index = index < 0 ? 0 : index > content.childCount - 1 ? content.childCount - 1 : index;
        float newX = Mathf.Round(index) * itemWidth;
        Vector3 newPos = new Vector3(-newX, 0, 0);

        snapCoroutine = CRSnap(newPos, snapTime);
        StartCoroutine(snapCoroutine);
    }

    IEnumerator CRSnap(Vector3 newPos, float duration)
    {
        float timePast = 0;
        Vector3 startPos = content.localPosition;
        Vector3 endPos = newPos;

        while (timePast < duration)
        {
            timePast += Time.deltaTime;
            float factor = timePast / duration;
            content.localPosition = Vector3.Lerp(startPos, endPos, factor);
            yield return null;
        }
        ServicesManager.Instance.SoundManager.PlayOneSound(ServicesManager.Instance.SoundManager.tick);
    }
}
