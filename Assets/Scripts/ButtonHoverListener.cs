using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverListener : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    private Transform topPart;
    private void Start()
    {
        topPart = transform.GetChild(0);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        topPart.DOKill();
        topPart.DOMoveY(transform.position.y + 10f, 0.25f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        topPart.DOKill();
        topPart.DOMoveY(transform.position.y + 6f, 0.25f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        topPart.DOKill();
        topPart.DOMoveY(transform.position.y, 0.1f).OnComplete(() =>
        {
            topPart.DOMoveY(transform.position.y + 6f, 0.1f); 
        });
    }

}
