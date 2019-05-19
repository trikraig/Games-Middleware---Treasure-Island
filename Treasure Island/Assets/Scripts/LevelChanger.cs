using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

    public Animator animator;
    
    private int levelToLoad;

    public static LevelChanger levelChanger;

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToQuit()
    {
        animator.SetTrigger("FadeOut");
        StartCoroutine(Quit());
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        FindObjectOfType<audioManager>().ChangeMusic(levelIndex);
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public static IEnumerator Quit()
    {
        yield return new WaitForEndOfFrame();
        Application.Quit();
    }
}
