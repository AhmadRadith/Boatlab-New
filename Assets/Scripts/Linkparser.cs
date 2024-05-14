using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TMP_Text))]

public class Linkparser : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler

{
    public TMP_Text text;
    public void OnPointerClick(PointerEventData eventData)
    {

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, null);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
            Application.OpenURL(linkInfo.GetLinkID());  
        }
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
    }
    
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
    }
}
