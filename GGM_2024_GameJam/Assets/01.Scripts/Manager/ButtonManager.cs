using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);      // 바로 씬 넘어감
    }

    public void OnExit()
    {
        Debug.Log("나가기 누름!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
