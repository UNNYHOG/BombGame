using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferGroup : IReturnToBuffer
{
    GameObject m_Prefab;
    Transform m_BufferTrm;
    HashSet<IBufferable> m_ActiveObjects = new HashSet<IBufferable>();
    LinkedList<IBufferable> m_DeactiveObjects = new LinkedList<IBufferable>();

    public void ReturnObjectToBuffer(IBufferable bufferable)
    {
        if (m_ActiveObjects.Remove(bufferable)) {
            m_DeactiveObjects.AddLast(bufferable);
            Transform trm = bufferable.GetTransform();
            if (trm != null)
                trm.SetParent(m_BufferTrm);
        }
        else
            Debug.LogError("There is no such active object " + bufferable);
    }

    private IBufferable InstantiateOneObject()
    {
        GameObject obj = GameObject.Instantiate(m_Prefab) as GameObject;
        IBufferable bufferable = obj.GetComponent<IBufferable>();
        bufferable.SetBufferGroup(this);
        return bufferable;
    }

    public BufferGroup(string name, Transform bufferTrm, int count = 1)
    {
        m_BufferTrm = bufferTrm;
        m_Prefab = Resources.Load<GameObject>(name);
        for (int i = 0; i < count; i++)
        {
            IBufferable bufferable = InstantiateOneObject();
            Transform trm = bufferable.GetTransform();
            if (trm != null)
                trm.SetParent(m_BufferTrm);
            m_DeactiveObjects.AddLast(bufferable);
        }
    }

    public IBufferable ActivateObject()
    {
        IBufferable obj;
        if (m_DeactiveObjects.Count > 0) {
            obj = m_DeactiveObjects.First.Value;
            m_DeactiveObjects.RemoveFirst();
        }
        else {
            obj = InstantiateOneObject();
        }

        m_ActiveObjects.Add(obj);
        return obj;
    }
}