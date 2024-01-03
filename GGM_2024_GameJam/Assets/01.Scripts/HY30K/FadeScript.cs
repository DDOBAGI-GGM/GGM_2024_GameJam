using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeScript : Singleton<FadeScript>
{
    private Image _fadeImage;

    public override void Awake()
    {
        base.Awake();
        _fadeImage = GetComponent<Image>();
    }

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        _fadeImage.DOFade(0, 3f);
    }

    public void FadeOut()
    {
        _fadeImage.DOFade(1, 3f);
    }
}
