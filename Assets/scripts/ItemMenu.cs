using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private Transform parentBeforeDrag;

    void Awake()
    {
        
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.position;
        parentBeforeDrag = transform.parent;
        transform.SetParent(parentBeforeDrag, true);
        canvasGroup.alpha = 0.6f; // Torna o item um pouco transparente
        canvasGroup.blocksRaycasts = false; // Permite que o item passe pelos raios de UI
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(parentBeforeDrag); // Retorna ao menu
        
        // Verifica se há um slot válido sob o item solto
        GameObject droppedOn = eventData.pointerEnter;
        if (droppedOn != null && droppedOn.CompareTag("Slot"))
        {
            Debug.Log("Dropped on: " + droppedOn.name);
            transform.SetParent(droppedOn.transform);
            rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            // Retorna à posição original se não houver um slot válido
            rectTransform.position = startPosition;
        }
    }
}
