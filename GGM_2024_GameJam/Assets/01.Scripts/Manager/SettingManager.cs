using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] private GameObject inGamePanel, settingPanel;
    [SerializeField] private Button settingBackBtn;
    [SerializeField] private bool esc = false;      // SerializeField 지워도 됨.

    [SerializeField] private int gameBuildIndex = 4;

    private void OnEnable()
    {
        //Debug.Log("씬 이동 이벤트 등록");
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void OnDisable()
    {
        //Debug.Log("씬 이동 이벤트 삭제");
        SceneManager.sceneLoaded -= LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name + "으로 변경되었습니다.");
        if (SceneManager.GetActiveScene().buildIndex >= gameBuildIndex)             // 이것들 게임일때만 ㅇㅇ
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
            settingBackBtn.onClick.AddListener(() => { IntroInit.Instance.SettingCancel(); });          // 이건 인트로일때만 아니니
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= gameBuildIndex)      // 게임씬일 때만. 
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
        if (SceneManager.GetActiveScene().buildIndex >= gameBuildIndex) inGamePanel.SetActive(!value);            // 게임씬일때만
        settingPanel.SetActive(value);
    }

    public void OnGame()
    {
        Debug.Log("게임누름");
        inGamePanel.SetActive(false);
        esc = !esc;
        Time.timeScale = 1;
        //UIManager.Instance.ChangeScene("Game");
    }
}


// 게임씬들은 다 빌드인덱스 3이상에 두기