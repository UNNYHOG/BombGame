using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObject : BaseObject {
    public string m_ExplosionEffectName;
    public float m_DamageRadius = 5;
    public float m_MaxDamage = 50;
    public float m_MinDamage = 1;
    public float m_WallDamageMultiplier = 0.5f;

    void OnCollisionEnter(Collision collision)
    {
        DealDamage();
        SimpleBuffer.CreateObjectAtPoint(m_ExplosionEffectName, GetTransform().localPosition);
        ReturnToBuffer();
    }

    void DealDamage() {
        Vector3 bombPos = GetTransform().localPosition;
        List<IDamageable> targets = CollisionsHelper.GetAllDamageableTargetsInArea(bombPos, m_DamageRadius);
        if (targets != null && targets.Count > 0) {
            for (int i = 0; i < targets.Count;i++) {
                Vector3 directionToTarget = targets[i].GetTransform().localPosition - bombPos;
                float distance = directionToTarget.magnitude;
                float damage = Mathf.Lerp(m_MaxDamage, m_MinDamage, distance / m_DamageRadius);
                Debug.Log(string.Format("Target {0}: distance = {1}, original damage = {2}", i, distance, damage));
                if (CollisionsHelper.CheckCollisionWithWall(bombPos, directionToTarget, distance)) {
                    Debug.Log("There is wall between -> decrease damage");
                    damage *= m_WallDamageMultiplier;
                }
                Debug.LogWarning(string.Format("Final Damage {0}", damage));
                targets[i].DealDamage(ref damage);
            }
        }
    }
}
