using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

using System.IO;

public class menu : MonoBehaviour
{
    public GameObject iniobjectcredit;
    public GameObject iniobjectcontrol;
    public GameObject CPDanTP;
    public TMP_Text UpdateNetwork;
    public TMP_Text VersionShow;
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
    void Start()
    {
        StartCoroutine(getRequest("https://raw.githubusercontent.com/AhmadRadith/Boatlab-New/master/Assets/StreamingAssets/versions"));
        VersionShow.text = "Versi:\n" + File.ReadAllText((Application.streamingAssetsPath + "/versions"));
    }

    void Update()
    {

    }
    IEnumerator getRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            UpdateNetwork.transform.localPosition = new Vector3(412.4f, UpdateNetwork.transform.localPosition.y);
            UpdateNetwork.color = Color.red;
            UpdateNetwork.text = "Gagal mendapat versi terbaru";    
        }
        else
        {
            if(uwr.downloadHandler.text != File.ReadAllText((Application.streamingAssetsPath + "/versions")))
            {
                UpdateNetwork.transform.localPosition = new Vector3(482.3f, -197.7f, 0f);
                //UpdateNetwork.color = Color.blue;
                //Vector3 p = new Vector3(482.3f, -197.7f, 0f);
                //UpdateNetwork.transform.position = p;
                UpdateNetwork.text = "<link=\"https://github.com/AhmadRadith/Boatlab-New/releases\">Perbarui Aplikasi</link>";
            }
            else
            {
                //UpdateNetwork.text = $"Screen Width:{Screen.width}\nScreen height: {Screen.height}";
                UpdateNetwork.text = "";
            }
        }
        StartCoroutine(FadeLoop());
    }
    private IEnumerator FadeLoop()
    {
        while (true)
        {
            yield return StartCoroutine(FadeOut());

            yield return new WaitForSeconds(0.6f);

            yield return StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeOut()
    {
        float currentTime = 0f;
        while (UpdateNetwork.color.a > 0f)
        {
        float alpha = Mathf.Lerp(1f, 0f, currentTime / 6.5f);
        UpdateNetwork.color = new Color(UpdateNetwork.color.r, UpdateNetwork.color.g, UpdateNetwork.color.b, alpha);
        currentTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float currentTime = 0f;
        while (UpdateNetwork.color.a < 1f)
        {
            float alpha = Mathf.Lerp(0f, 1f, currentTime / 2f);
            UpdateNetwork.color = new Color(UpdateNetwork.color.r, UpdateNetwork.color.g, UpdateNetwork.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}
