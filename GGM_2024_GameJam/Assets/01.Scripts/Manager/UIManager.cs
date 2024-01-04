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

    private Sequence _sequenceFadeInOut;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        imageIdx = 0;
        if (cutSceneViewer)
            PlayCutScene();
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PlayCutScene()
    {
        if (imageIdx < cutSceneImages.Length)
            cutSceneViewer.sprite = cutSceneImages[imageIdx++];
        else
            ChangeScene(sceneName);

        //FadeInOut();
    }

    private void FadeInOut()
    {
        _sequenceFadeInOut.Restart();
        imageIdx++;
    }
}
