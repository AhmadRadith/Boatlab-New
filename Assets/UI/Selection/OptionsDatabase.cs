using UnityEngine;

[CreateAssetMenu]
public class OptionsDatabase : ScriptableObject
{
    public Character[] options;
    public int OptionsCount
    {
        get
        {
            return options.Length;
        }
    }
    public Character GetOption(int index)
    {
        return options[index];
    }

    public Character[] GetAllOptions()
    {
        return options;
    }
}