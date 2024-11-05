using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{
    static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindFirstObjectByType<T>();
                if (m_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    m_instance = obj.AddComponent<T>();
                }
            }
            return m_instance;   
        }
    }
 
    public virtual void Awake()
    {
        if (m_instance == null)
            m_instance = this as T;
        else
            Destroy(gameObject);
    }
}
