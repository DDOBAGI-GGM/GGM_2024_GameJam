using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Fade")]
    [SerializeField] private Image fadePanel;
    [SerializeField] private float duration;
    [SerializeField] private Ease easingFunc;

    private void Start()
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(0, duration).SetEase(easingFunc).OnComplete(() => fadePanel.gameObject.SetActive(false));
    }
    public void ChangeScene(string sceneName)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(1, duration).OnComplete(() => { SceneManager.LoadScene(sceneName); }).SetEase(easingFunc);
    }
}
