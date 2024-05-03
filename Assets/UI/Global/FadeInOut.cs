using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public Animator animator;
    public static int levelToLoad;
    void Start()
    {
        levelToLoad = 2;
        animator = GameObject.FindWithTag("FadeUI").GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                FadeToLevel(0);
            }
            else if (SceneManager.GetActiveScene().buildIndex > 1)
            {
                FadeToLevel(1);
            }
        }
    }
    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void FadeToLevelUsingPublicVar()
    {
        animator.SetTrigger("FadeOut");

    }
    public void ChangeLevelIndex(int newLevelIndex)
    {
        levelToLoad = newLevelIndex;
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
