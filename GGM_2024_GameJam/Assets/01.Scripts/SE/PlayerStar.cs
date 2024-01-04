using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStar : MonoBehaviour
{
    private PlayerSupporter playerSupporter;
    private List<GameObject> starList = new List<GameObject>();
    private int nowStarCount = 0;

    [SerializeField] private float starSize = 0.75f;

    public static PlayerStar Instance;

    private void Awake()
    {
        Instance = this;
        playerSupporter = GetComponent<PlayerSupporter>();
    }

    public void StarAdd(Transform star)
    {
        star.parent = playerSupporter.supportersList[nowStarCount].gameObject.transform;      // 자식으로 넣어줌.
        nowStarCount++;
        star.localScale = new Vector3(starSize, starSize, starSize);
        star.localPosition = new Vector3(0, 0.5f, 0);
        starList.Add(star.gameObject);
    }

    public void UseStar()
    {
        for (int i = 0; i < starList.Count; i++)
        {
            Destroy(starList[i].gameObject);
        }
        starList.Clear();
    }
}
