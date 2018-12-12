using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestruction : MonoBehaviour, IBufferable
{
    public float m_DestroyTime = 5;

    void OnEnable() {
        StartCoroutine(DestroyObject(m_DestroyTime));
    }

    IEnumerator DestroyObject(float delay) {
        yield return new WaitForSeconds(delay);
        ReturnToBuffer();
    }

#region IBufferable
    Transform m_Transform;
    IReturnToBuffer m_BufferGroupParent;

    public void SetBufferGroup(IReturnToBuffer group) {
        m_BufferGroupParent = group;
    }

    public IReturnToBuffer GetBufferGroup() {
        return m_BufferGroupParent;
    }

    public void ReturnToBuffer() {
        SimpleBuffer.ReturnToBuffer(this);
    }

    public Transform GetTransform()
    {
        return m_Transform;
    }

    void Awake() {
        m_Transform = transform;   
    }
#endregion
}
