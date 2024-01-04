using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] private GameObject inGamePanel, settingPanel;
    [SerializeField] private Button settingBackBtn;
    [SerializeField] private bool esc = false;      // SerializeField ������ ��.

    [SerializeField] private int gameBuildIndex = 4;

    private void OnEnable()
    {
        //Debug.Log("�� �̵� �̺�Ʈ ���");
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void OnDisable()
    {
        //Debug.Log("�� �̵� �̺�Ʈ ����");
        SceneManager.sceneLoaded -= LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name + "���� ����Ǿ����ϴ�.");
        if (SceneManager.GetActiveScene().buildIndex >= gameBuildIndex)             // �̰͵� �����϶��� ����
        {
            settingBackBtn.onClick.RemoveAllListeners();
            settingBackBtn.onClick.AddListener(() => { OnSetting(false); });
            settingBackBtn.onClick.AddListener(() => { SoundManager.Instance.PlaySFX("button"); });

            SoundManager.Instance.PlayBGM("game");
        }
        else
        {
            settingBackBtn.onClick.RemoveAllListeners();
            settingBackBtn.onClick.AddListener(() => { OnSetting(false); });
        }

        if (SceneManager.GetActiveScene().name == "CutScene")
        {
            SoundManager.Instance.PlayBGM("cut");
        }

        if (SceneManager.GetActiveScene().name == "Intro")
        {
            settingBackBtn.onClick.AddListener(() => { IntroInit.Instance.SettingCancel(); });          // �̰� ��Ʈ���϶��� �ƴϴ�
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= gameBuildIndex)      // ���Ӿ��� ����. 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                esc = !esc;
                inGamePanel.SetActive(esc);
                Time.timeScale = 0;
            }
        }
    }

    public void OnSetting(bool value)
    {
        if (SceneManager.GetActiveScene().buildIndex >= gameBuildIndex) inGamePanel.SetActive(!value);            // ���Ӿ��϶���
        settingPanel.SetActive(value);
    }

    public void OnGame()
    {
        Debug.Log("���Ӵ���");
        inGamePanel.SetActive(false);
        esc = !esc;
        Time.timeScale = 1;
        //UIManager.Instance.ChangeScene("Game");
    }
}


// ���Ӿ����� �� �����ε��� 3�̻� �α�