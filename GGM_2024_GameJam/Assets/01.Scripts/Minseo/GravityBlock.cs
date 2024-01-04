using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBlock : MonoBehaviour
{
    

    [SerializeField] private float pushForce = 2f; 

    public bool issf = false;

    private void Awake()
    {
        //_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Freeze();
    }

    private void Freeze()
    {
        ////if (!GameManager.Instance.Is3D)
        //if (issf)
        //{
        //    _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        //    _rigidbody.constraints = RigidbodyConstraints.FreezePositionX;

            
        //}
        //else
        //{
        //    _rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        //    _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;   
        //}
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody _rigidbody = hit.collider.attachedRigidbody;

        if(_rigidbody != null)
        {
            Vector3 forceDir = hit.gameObject.transform.position - transform.position;
            forceDir.y = 0;
            forceDir.Normalize();
            
            _rigidbody.AddForceAtPosition(forceDir * pushForce, transform.position, ForceMode.Impulse);
        }
    }
}
