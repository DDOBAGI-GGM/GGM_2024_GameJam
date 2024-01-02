using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour       // �׳� �̱���
{
    static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                return null;        // ����� ���� �׳� �̰� ���߿� ������ �ǰ� .... 

                instance = GameObject.FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    instance = singleton.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);            // ���� �ƴϸ� �ڽ��� ������.
        }
    }
}

/*
�� �̱����� ���ַ���  MonoBehaviour �ڸ��� Singleton<'��ũ��Ʈ �̸�'> �̶�� �����ֱ�
 */