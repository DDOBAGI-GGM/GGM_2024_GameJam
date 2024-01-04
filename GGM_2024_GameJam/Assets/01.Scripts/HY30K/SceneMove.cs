using Collections.Shaders.CircleTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("튜토 따까리 초기화");
            var supporter = other.gameObject.GetComponent<PlayerSupporter>();
            if (supporter != null)
            {
                supporter.TutorialReSet();
            }
        }
    }
}
