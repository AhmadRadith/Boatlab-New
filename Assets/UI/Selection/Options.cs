using System;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string LearningMaterialName;
    public string LearningMaterialShortDesc;
    public Sprite LearningMaterialCover;
    public bool enableStartButton;
    public bool enableQuizButton;
    public QuizManager.AvailableOptions QuizOption;
    public int teleportToScene;
}
