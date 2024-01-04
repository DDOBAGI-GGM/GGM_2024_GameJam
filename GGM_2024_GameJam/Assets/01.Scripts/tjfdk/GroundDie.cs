using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDie : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.transform.name);
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerMovement>().IsDead = true;
        }
    }
}
