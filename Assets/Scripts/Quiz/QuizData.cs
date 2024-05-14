using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizData : MonoBehaviour
{

    public class SingleQData
    {
        public string question;
        public string type;
        public List<string> options;
        public string answer;
    }
    public class QData
    {
        public List<SingleQData> data;
    }
}
