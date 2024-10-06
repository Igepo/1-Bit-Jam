using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform target;
    public RectTransform arrow;
    public Vector2 arrowOffset = new Vector2(80, 80);

    void Update()
    {
        if (target == null) return;

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(target.position);
        Vector2 anchoredPosition = new Vector2(screenPoint.x, screenPoint.y);

        if (screenPoint.z < 0 || !IsInViewport(screenPoint))
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);

            float leftEdge = 0 + arrowOffset.x;
            float rightEdge = screenSize.x - arrowOffset.x;
            float topEdge = screenSize.y - arrowOffset.y;
            float bottomEdge = 0 + arrowOffset.y;

            if (screenPoint.x < 0)
            {
                anchoredPosition.x = leftEdge;
            }
            else if (screenPoint.x > screenSize.x)
            {
                anchoredPosition.x = rightEdge;
            }
            else
            {
                anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, leftEdge, rightEdge);
            }

            if (screenPoint.y < 0)
            {
                anchoredPosition.y = bottomEdge;
            }
            else if (screenPoint.y > screenSize.y)
            {
                anchoredPosition.y = topEdge;
            }
            else
            {
                anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, bottomEdge, topEdge);
            }

            arrow.gameObject.SetActive(true);
            arrow.position = new Vector3(anchoredPosition.x, anchoredPosition.y, 0);
            //arrow.rotation = Quaternion.LookRotation(Vector3.forward, (target.position - Camera.main.transform.position).normalized);
        }
        else
        {
            arrow.gameObject.SetActive(false);
        }
    }

    private bool IsInViewport(Vector3 screenPoint)
    {
        return screenPoint.x >= 0 && screenPoint.x <= Screen.width && screenPoint.y >= 0 && screenPoint.y <= Screen.height;
    }
}
