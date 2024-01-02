using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [Header("스프라이트 회전축 고정")][SerializeField] private bool freezeXZAxis = true;

    private void Update()
    {
        if (freezeXZAxis)
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        else
            transform.rotation = Camera.main.transform.rotation;
    }
}
