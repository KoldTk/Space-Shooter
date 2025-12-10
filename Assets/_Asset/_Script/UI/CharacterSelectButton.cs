using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectButton : MenuButton
{
    public string sceneName;
    [SerializeField] private GameObject characterPrefab;
    private void SwitchScene()
    {
        if (sceneName == "")
        {
            sceneName = GameManager.Instance.selectedChapter;
        }
        Time.timeScale = 1;
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            GameManager.Instance.ClearObject();
            Debug.Log($"Loading progress: {asyncLoad.progress}");
            yield return null;
        }
    }

    public override void ClickEvent()
    {
        GameManager.Instance.selectedCharacter = characterPrefab;
        SwitchScene();
    }
}
