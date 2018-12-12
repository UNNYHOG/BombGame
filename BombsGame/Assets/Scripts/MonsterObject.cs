using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterObject : BaseObject, IDamageable {

    public float m_HitPoints;

    private MobAnimation m_Animation;
    private Collider m_Collider;

    private const int DefaultState = 0;
    private const int m_DeadState = 1<<1;
    private int m_State = DefaultState;

    public bool IsDead() {
        return (m_State & m_DeadState) != 0;
    }

    private void KillMonster() {
        m_HitPoints = 0;
        m_State |= m_DeadState;
        m_Collider.enabled = false;
    }

    protected override void Awake()
    {
        base.Awake();
        m_Animation = GetComponent<MobAnimation>();
        m_Collider = GetComponent<Collider>();
    }

    public void DealDamage(ref float damage) {
        if (m_HitPoints <= damage) {
            damage = m_HitPoints;
            KillMonster();
            m_Animation.PlayDeathAnimation();
        } else {
            m_HitPoints -= damage;
            m_Animation.PlayDamageAnimation();
        }
    }
}
