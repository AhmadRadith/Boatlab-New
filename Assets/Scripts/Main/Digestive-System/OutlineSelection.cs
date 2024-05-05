using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OutlineSelection : MonoBehaviour
{

    public GameObject humanBody;
    public GameObject bodyOrgan;
    public TMP_Text TextDesc;
    public GameObject BodyOrganDescriptionText;
    public GameObject BodyOrganDescriptionBox;
    //public GameObject DetectionBodyOrganDescriptionBox;
    public string EmptyString;
    public bool Useblack;
    private bool enableUIdesc;
    private string UIdesc;
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;
    private GameObject KeptGO;
    readonly float maxdistance = 34;
    public void ToggleBody()
    {
      bodyOrgan.SetActive(humanBody.activeSelf);          
      humanBody.SetActive(!humanBody.activeSelf);
    }
    private void LateUpdate()
    {
        if (enableUIdesc)
        {
            TextDesc.enabled = true;
            TextDesc.text = UIdesc;
            Vector3 A = Input.mousePosition;
            A.x += 120;
            A.y -= 65;
            TextDesc.transform.position = A ;
            return;
        }
            TextDesc.enabled = false;
    }
    void Update()
    {

        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (highlight)
        {
           UIdesc = "";
           enableUIdesc = false;
            if (!KeptGO || KeptGO != highlight.gameObject)
            {
                highlight.gameObject.GetComponent<Outline>().enabled = false;
                highlight = null;
            }
        }
        if (!EventSystem.current.IsPointerOverGameObject()
            && Physics.Raycast(ray, out raycastHit))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    if (Vector3.Distance(cursorPosition, highlight.gameObject.transform.position) < maxdistance)
                    {
                        UIdesc = highlight.gameObject.name;
                        enableUIdesc = true;
                        highlight.gameObject.GetComponent<Outline>().OutlineColor = (Useblack ? Color.black : Color.white);
                        highlight.gameObject.GetComponent<Outline>().enabled = true;
                    }
                }
                else
                {
                    UIdesc = highlight.gameObject.name;
                    enableUIdesc = true;
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = (Useblack ? Color.black: Color.white);
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                UIdesc = "";
                enableUIdesc = false;
                highlight = null;
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            if (highlight)
            {
                if (Vector3.Distance(cursorPosition, highlight.gameObject.transform.position) < maxdistance)
                {
                    if(KeptGO)
                    {
                        KeptGO.GetComponent<Outline>().enabled = false;
                        KeptGO = null;

                    }
                    string desc;
                    try
                    {
                        desc = File.ReadAllText((Application.streamingAssetsPath + $"/Explanations/{SceneManager.GetActiveScene().name.ToLower()}/{highlight.gameObject.name.ToLower()}.txt"));
                    }
                    catch
                    {
                        desc = EmptyString == "" ?  "Organ yang ini itu bukan organ utama, soalnya organ ini nggak dilalui makanan. Tapiii organ ini itu membantu dalam proses pencernaan makanan, dengan cara ngirimin enzim yang berguna dalam pencernaan makanan.\r\n" : EmptyString;
                    }
                    BodyOrganDescriptionBox.SetActive(true);
                    TMP_Text thetext = BodyOrganDescriptionText.GetComponentInChildren<TMP_Text>();
                    thetext.text = desc;
                    KeptGO = highlight.gameObject;
                }
            }
            else
            {
                if(KeptGO)
                {
                        KeptGO.GetComponent<Outline>().enabled = false;
                        KeptGO = null;
                }
                if(!EventSystem.current.IsPointerOverGameObject())
                {
                    BodyOrganDescriptionBox.SetActive(false);
                }
            }
        }
    }
    public void OnHoverShowDesc(string desc)
    {
        UIdesc = desc;
        enableUIdesc = true;
        TextDesc.enabled = true;
        TextDesc.text = UIdesc;

    }
    public void OnHoverExitHideDesc()
    {
        enableUIdesc = false;
        TextDesc.enabled = false;
    }
}
