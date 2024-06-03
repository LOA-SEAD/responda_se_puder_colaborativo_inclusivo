using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class botaoInteracao : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 scale = new Vector3(0.8192071f, 2.130745f, 1.291361f);
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = scale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

}
