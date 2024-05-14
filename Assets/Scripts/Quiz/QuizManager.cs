using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Newtonsoft.Json;
using TMPro;
using System.Linq;
using UnityEngine.UI;
public class QuizManager : MonoBehaviour
{
    //private loaded
    public static AvailableOptions SelectedOption;
    private static List<QuizData.SingleQData> ListOfJson;
    public GameObject MainMenuObject;
    public GameObject ScrollableTextMain;
    public GameObject ScrollableJawabanText;
    public GameObject QuestionsParent;
    public GameObject clonedUiQuestion;
    public GameObject SubmitButton;
    public TMP_Text countdownText;
    public float countdownTimer;
    private bool countdownOn = true;
    private Dictionary<string, string>   DictionaryAnswer;
    public enum AvailableOptions
    {
        None,
        DigestiveSystem,
    }

    void Start()
    {
        DictionaryAnswer = new Dictionary<string, string>();
        //ListOfJson = new List<QuizData.SingleQData>();
        print("Entered The Quiz");
        //if(SelectedOption == AvailableOptions.None)
        //{
        //    SelectedOption = AvailableOptions.DigestiveSystem;
        //}
        var desc = File.ReadAllText((Application.streamingAssetsPath + $"/Quiz/{SelectedOption.ToString().ToLower()}.json"));
        ListOfJson = JsonConvert.DeserializeObject<List<QuizData.SingleQData>>(desc);
        foreach (var item in ListOfJson.Select((value, i) => new { i, value }))
        {
            var value = item.value;
            var index = item.i;
            var data = Instantiate(clonedUiQuestion, QuestionsParent.transform);
            data.name = $"question{(index+1).ToString()}";
            data.GetComponentsInChildren<TMP_Text>().Last().text = $"{(index+1).ToString()} | {value.question.ToString()}";
            foreach (var optionsitem in value.options.Select((value, i) => new { i, value }))
            {
                var optionvalue = optionsitem.value;
                var optionindex = optionsitem.i;    
                var aw = data.GetComponentsInChildren<Button>()[optionindex];
                aw.GetComponentInChildren<TMP_Text>().text = optionvalue.ToString();
                aw.name = $"question{(index + 1).ToString()}";
            }
         }
        SubmitButton.transform.SetAsLastSibling();
    }
    public void StartQuiz()
    {
        MainMenuObject.SetActive(false);
        ScrollableTextMain.SetActive(true);
    }

    public void SelectAnswer(GameObject aaa)
    {
        var current = aaa.GetComponentInChildren<TMP_Text>().text;
        if (DictionaryAnswer.TryGetValue(aaa.name, out string _))
        {
            DictionaryAnswer[aaa.name] = current;
        }
        else
        {
            DictionaryAnswer.Add(aaa.name, current);
        }
    }

    private void Update()
    {
        if (countdownOn)
        {
            if (countdownTimer > 0)
            {
                countdownTimer -= Time.deltaTime;
                updateTimer(countdownTimer);
            }
            else
            {
                StartQuiz();
                countdownText.text = "";
                countdownTimer = 0;
                countdownOn = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float seconds = Mathf.FloorToInt(currentTime % 60);

        countdownText.text = seconds.ToString();
    }

    public void cekSoal()
    {
        ScrollableTextMain.SetActive(false);
        ScrollableJawabanText.SetActive(true);
        var ii = ScrollableJawabanText.GetComponentInChildren<TMP_Text>();
        print(ii.text);
        foreach (var item in ListOfJson.Select((value, i) => new { i, value }))
        {
            var value = item.value;
            var index = item.i;
            string vv;

            if (DictionaryAnswer.TryGetValue($"question{index + 1}", out vv))
            {
                if(vv != value.answer)
                {
                    ii.text += ($"{(index+1).ToString()} | {value.question} | {vv} jawaban Salah yang benar adalah <b>{value.answer}</b>\n\n");
                }
                else
                {
                    ii.text += ($"{(index + 1).ToString()} | {value.question} | <b>{vv}</b> adalah jawaban yang benar\n\n");
                }
            }
            else
            {
                ii.text += ($"{(index + 1).ToString()} | {value.question} | jawaban kosong\n\n");
            }
        }
        ii.text = "\n" + ii.text.Trim();
    }
}
