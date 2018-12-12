using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable : ITransform{
    void DealDamage(ref float damage);
}
