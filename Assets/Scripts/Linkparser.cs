using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TMP_Text))]

public class Linkparser : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler

//class Linkparser : MonoBehaviour
{
    public TMP_Text text;
    private TMP_Text _tmpTextBox;
    //private void Awake()
    //{
    //    print("Nigga");
    //    print(gameObject.GetComponent<TMP_Text>());
    //    _tmpTextBox = GetComponent<TMP_Text>();
    //}
    //
    public void OnPointerClick(PointerEventData eventData)
    {
        print("Get");   
        print(_tmpTextBox);

        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, null);
        print(linkIndex);
        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
            Application.OpenURL(linkInfo.GetLinkID());  
        }
    }
    public void OpenLink(string ww)
    {
        Application.OpenURL(ww);
    }
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
    
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
    }
}
