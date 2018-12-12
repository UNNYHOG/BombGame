using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBufferable : ITransform{
    void SetBufferGroup(IReturnToBuffer group);//Used instead of searching for a group
    IReturnToBuffer GetBufferGroup();
    void ReturnToBuffer();
}
