using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class menu : MonoBehaviour
{
    public GameObject iniobjectcredit;
    public GameObject iniobjectcontrol;
    public GameObject CPDanTP;
    public TMP_Text UpdateNetwork;
    public void CreditEnabled()
    {
        iniobjectcredit.SetActive(!iniobjectcredit.activeSelf);
    }
    public void ControlEnable()
    {
        iniobjectcontrol.SetActive(!iniobjectcontrol.activeSelf);
    }
    public void CPTPEnable()
    {
        CPDanTP.SetActive(!CPDanTP.activeSelf);
        //CPDanTP.SetActive(toggle);
    }
    public void Exit()
    {
        Debug.Log(GetComponent<Animator>());
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    // Start is called before the first frame update-o]
    void Start()
    {
        StartCoroutine(getRequest("https://raw.githubusercontent.com/AhmadRadith/Boatlab-New/master/Assets/versions"));
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator getRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }
}
