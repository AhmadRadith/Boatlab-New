using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class menu : MonoBehaviour
{
    public GameObject iniobjectcredit;
    public GameObject iniobjectcontrol;
    public GameObject CPDanTP;
    public void CreditEnabled(bool toggle)
    {
        iniobjectcredit.SetActive(toggle);
    }
    public void ControlEnable(bool toggle)
    {
        iniobjectcontrol.SetActive(toggle);
    }
    public void CPTPEnable()
    {
        print(CPDanTP);
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
