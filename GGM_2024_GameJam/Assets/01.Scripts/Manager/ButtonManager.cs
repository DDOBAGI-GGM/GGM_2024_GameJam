using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);      // �ٷ� �� �Ѿ
    }

    public void OnPlayButtonSound()     // �����ص����� ���� ���� ����
    {
        SoundManager.Instance.PlaySFX("button");
    }

    public void OnExit()
    {
        //Debug.Log("������ ����!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
