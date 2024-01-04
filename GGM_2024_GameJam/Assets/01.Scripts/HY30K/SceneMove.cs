using Collections.Shaders.CircleTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CircleTransition.Instance.CloseBlackScreen();
        Invoke("MoveScene", 1f);
    }

    void MoveScene()
    {
        SceneManager.LoadScene("Game");
    }
}
