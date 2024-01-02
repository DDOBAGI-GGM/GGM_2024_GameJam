using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArrowChoose : MonoBehaviour
{
    [SerializeField] private Button[] interactor = new Button[3];     // 3이든 뭐든~
    private TextMeshProUGUI[] interactorText = new TextMeshProUGUI[3];
    [SerializeField] private string[] text = new string[3];
    private int count, max;

    private void Start()
    {
        max = interactor.Length;
        count = 0;

        for (int i = 0; i < interactor.Length; i++)
        {
            interactorText[i] = interactor[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (count + 1 >= max) return;    // 플러스 한 것이 크면 리턴

            interactorText[count].text = text[count];
            count++;
            interactorText[count].text = $"● {text[count]}";
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (count - 1 < 0) return;    // 마이너스 한 것이 크면 리턴

            interactorText[count].text = text[count];
            count--;
            interactorText[count].text = $"● {text[count]}";
        }

        if (Input.GetKeyDown(KeyCode.Return))       // 엔터키
        {
            interactor[count].onClick?.Invoke();
        }
    }
}
