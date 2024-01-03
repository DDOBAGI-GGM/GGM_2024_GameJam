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

    private void Start()
    {
        //inGamePanel.SetActive(esc);
        Debug.Log(SceneManager.GetActiveScene().name);
    }

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
        //Debug.Log(scene.name + "으로 변경되었습니다.");
        if (SceneManager.GetActiveScene().name == "SE")
        {
            settingBackBtn.onClick.RemoveAllListeners();
            settingBackBtn.onClick.AddListener(() => { OnSetting(false); });
            settingBackBtn.onClick.AddListener(() => { IntroInit.Instance.SettingCancel(); });
        }
        else
        {
            settingBackBtn.onClick.RemoveAllListeners();
            settingBackBtn.onClick.AddListener(() => { OnSetting(false); });
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")      // 게임씬일 때만. 임의로 SE 로 해줌.
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                esc = !esc;
                inGamePanel.SetActive(esc);
            }
        }
    }

    public void OnSetting(bool value)
    {
        if (SceneManager.GetActiveScene().name == "Game") inGamePanel.SetActive(!value);            // 게임씬일때만
        settingPanel.SetActive(value);
    }

    public void OnGame()
    {
        Debug.Log("게임누름");
        inGamePanel.SetActive(false);
        esc = !esc;
        UIManager.Instance.ChangeScene("tjfdk(obj)");
    }
}
