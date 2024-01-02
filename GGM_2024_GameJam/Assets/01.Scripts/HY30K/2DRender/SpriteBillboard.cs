using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [Header("��������Ʈ ȸ���� ����")][SerializeField] private bool freezeXZAxis = true;

    private void Update()
    {
        if (freezeXZAxis)
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        else
            transform.rotation = Camera.main.transform.rotation;
    }
}
