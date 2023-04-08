using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                    DontDestroyOnLoad(instance.gameObject);
                    if (instance != null)
                    {
                        Debug.LogError("���� ������" + typeof(T) + "�� Ȱ��ȭ �� �� �����ϴ�.");
                    }
                }
                return instance;
            }
        }

        private void Awake()
        {
            if(instance != null)
            {
                if(instance != this)
                {
                    Destroy(this.gameObject);
                }
                return;
            }
            instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {

        }

        void Update()
        {

        }
    }
}
