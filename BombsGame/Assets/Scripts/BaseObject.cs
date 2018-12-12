using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour, IBufferable {
    private Transform m_Transform;

#region IBufferable
    private IReturnToBuffer m_BufferGroup;

    public void ReturnToBuffer() {
        SimpleBuffer.ReturnToBuffer(this);
    }

    public void SetBufferGroup(IReturnToBuffer group) {
        m_BufferGroup = group;
    }

    public IReturnToBuffer GetBufferGroup() {
        return m_BufferGroup;
    }

    public Transform GetTransform() {
        return m_Transform;
    }

#endregion

    protected virtual void Awake() {
        m_Transform = transform;   
    }
}
