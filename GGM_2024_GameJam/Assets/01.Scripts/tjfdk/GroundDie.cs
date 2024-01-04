using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDie : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        if (other.transform.CompareTag("Player"))
        {
        Debug.Log(other.transform.name);
            other.transform.GetComponent<PlayerMovement>().IsDead = true;
            other.transform.GetComponent<PlayerMovement>().ResetPosition();
        }
    }
}
