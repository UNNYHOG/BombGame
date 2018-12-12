using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimation : MonoBehaviour, IAnimation {

    public static int Animation_Damage = Animator.StringToHash("Hit");
    public static int Animation_Dead = Animator.StringToHash("Die");

    public Animator m_Animator;

    void Awake() {
        m_Animator = GetComponentInChildren<Animator>();
    }

    public void PlayDamageAnimation() {
        m_Animator.SetTrigger(Animation_Damage);
    }

    public void PlayDeathAnimation() {
        m_Animator.SetBool(Animation_Dead, true);
    }
}
