using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonInteracton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector2(1.1f, 1.1f), .2f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(new Vector2(1f, 1f), .2f);
    }
}
