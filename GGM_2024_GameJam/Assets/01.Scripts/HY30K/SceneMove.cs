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
            Debug.Log("Ʃ�� ��� �ʱ�ȭ");
            var supporter = other.gameObject.GetComponent<PlayerSupporter>();
            if (supporter != null)
            {
                supporter.TutorialReSet();
            }
        }
    }
}
