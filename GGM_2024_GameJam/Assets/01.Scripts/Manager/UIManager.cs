using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("CutScene")]
    [SerializeField] private Sprite[] cutSceneImages;
    [SerializeField] private Image cutSceneViewer;
    [SerializeField] private string sceneName;
    private int imageIdx;

    [Header("Fade")]
    [SerializeField] private Image fadePanel;
    [SerializeField] private float duration;
    [SerializeField] private Ease easingFunc;

    private Sequence _sequenceFadeInOut;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
        fadePanel.gameObject.SetActive(true);
    }

    private void Start()
    {
/*        Color alpha = fadePanel.color;
        alpha.a = 1;
        fadePanel.color = alpha;*/

        fadePanel.DOFade(0, duration).SetEase(easingFunc).OnComplete(() => fadePanel.gameObject.SetActive(false));

        imageIdx = 0;
        if (cutSceneViewer)
            PlayCutScene();

        _sequenceFadeInOut = DOTween.Sequence()
            .SetAutoKill(false)
            .OnRewind(() =>
            {
                fadePanel.gameObject.SetActive(true);
            })
            .Append(fadePanel.DOFade(1, 1))
            .Append(fadePanel.DOFade(0, 1))
            .OnComplete(() =>
            {
                fadePanel.gameObject.SetActive(false);
            });
    }
    public void ChangeScene(string sceneName)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.DOFade(1, duration).OnComplete(() => { SceneManager.LoadScene(sceneName); }).SetEase(easingFunc);
    }

    public void PlayCutScene()
    {
        if (imageIdx < cutSceneImages.Length)
            cutSceneViewer.sprite = cutSceneImages[imageIdx];
        else
            ChangeScene(sceneName);

        FadeInOut();
    }

    private void FadeInOut()
    {
        _sequenceFadeInOut.Restart();
        imageIdx++;
    }
}
