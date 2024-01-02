using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    [TextArea] public string description;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
