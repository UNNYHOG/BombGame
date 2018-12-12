using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBuffer {

    Dictionary<string, BufferGroup> m_AllBufferGroups = new Dictionary<string, BufferGroup>();

    private BufferGroup PrepareObjects(string name, int count = 1) {
        return new BufferGroup(name, m_PrefabBufferTransform, count);
    }

    GameObject m_PrefabBufferObject;
    Transform m_PrefabBufferTransform;

    public SimpleBuffer() {
        m_PrefabBufferObject = new GameObject("PrefabBuffer");
        m_PrefabBufferObject.SetActive(false);
        GameObject.DontDestroyOnLoad(m_PrefabBufferObject);
        m_PrefabBufferTransform = m_PrefabBufferObject.transform;
    }

    static SimpleBuffer m_Instance;

    public static SimpleBuffer Initialize() {
        if (m_Instance == null)
            m_Instance = new SimpleBuffer();
        return m_Instance;
    }

    public static IBufferable CreateObjectAtPoint(string name, Vector3 pos) {
        IBufferable bufferable = GetObject(name);
        if (bufferable != null) {
            Transform trm = bufferable.GetTransform();
            trm.SetParent(null);
            trm.localPosition = pos;
        }
        return bufferable;
    }

    public static IBufferable GetObject(string name) {
        
        if (m_Instance != null) {
            BufferGroup group;
            if (!m_Instance.m_AllBufferGroups.TryGetValue(name, out group)) {
                group = m_Instance.PrepareObjects(name);
                m_Instance.m_AllBufferGroups.Add(name, group);
            }
            
            return group.ActivateObject();
        } else {
            Debug.LogError("Buffer wasn't initialized");
            return null;
        }
    }

    public static void ReturnToBuffer(IBufferable bufferable) {
        bufferable.GetBufferGroup().ReturnObjectToBuffer(bufferable);
    }

    public static void PrepareTestSceneObjects() {
        if (m_Instance == null)
            Initialize();
        
        m_Instance.PrepareObjects("MobSiege", 10);
        m_Instance.PrepareObjects("Bomb", 10);
        m_Instance.PrepareObjects("Explosion", 5);
    }
}
