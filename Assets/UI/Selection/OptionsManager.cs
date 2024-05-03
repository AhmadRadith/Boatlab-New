using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OptionsManager : MonoBehaviour
{
    public OptionsDatabase optionsDB;
    public TMP_Text nameText;
    public TMP_Text descText;
    public Image artworkSpritePlace;
    public Image IconImage;
    public Image LockImage;
    public Button StartButton;
    private int selectorOption = 0;

    private void Start()
    {
        UpdateCharacter(selectorOption);
    }
    public void NextOption()
    {
        selectorOption++;
        if (selectorOption >= optionsDB.OptionsCount)
        {
            selectorOption = 0;
        }
        UpdateCharacter(selectorOption);
    }
    public void BackOption()
    {
        selectorOption--;
        if (selectorOption < 0)
        {
            selectorOption = optionsDB.OptionsCount - 1;
        }
        UpdateCharacter(selectorOption);

    }
    private void UpdateCharacter(int selectedOption)
    {
        Character character = optionsDB.GetOption(selectedOption);
        Button button = StartButton.GetComponent<Button>();

        if (character.LearningMaterialCover != null)
        {
            artworkSpritePlace.color = new Color(1, 1, 1, 1);
            artworkSpritePlace.sprite = character.LearningMaterialCover;

        }
        else
        {
            artworkSpritePlace.sprite = character.LearningMaterialCover;
            artworkSpritePlace.color = Color.black;

        }
        nameText.text = character.LearningMaterialName;
        if (!string.IsNullOrEmpty(character.LearningMaterialShortDesc))
        {
            descText.text = character.LearningMaterialShortDesc;
        }
        else
        {
            descText.text = "Eh fitur ini sedang dalam pembuatan, tunggu di next update ya!";
        }

        button.interactable = character.enableStartButton;
        LockImage.enabled = !character.enableStartButton;
        FadeInOut.levelToLoad = character.teleportToScene;
    }
}
