using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsHelper {

    const int Layer_Ground = 9;
    const int Layer_Bomb = 10;
    const int Layer_Wall = 11;
    const int Layer_Mob = 12;

    const int DamageableLayer = 1 << Layer_Mob;
    const int WallLayer = 1 << Layer_Wall;

    public static int GetClickLayer(){
        return 1 << Layer_Ground;
    }

    const int CollidersBufferSize = 10;
    static Collider[] m_CollidersBuffer = new Collider[CollidersBufferSize];

    public static List<IDamageable> GetAllDamageableTargetsInArea(Vector3 point, float radius) {
        int layer = DamageableLayer;
        int count = Physics.OverlapSphereNonAlloc(point, radius, m_CollidersBuffer, layer);
        if (count > 0) {
            List<IDamageable> list = new List<IDamageable>();
            for (int i = 0; i < count;i++) {
                IDamageable damageable = m_CollidersBuffer[i].GetComponent<IDamageable>();
                if (damageable != null) {
                    list.Add(damageable);
                }
            }
            return list;
        }
        return null;
    }

    public static bool CheckCollisionWithWall(Vector3 origin, Vector3 direction, float distance)
    {
        int layer = WallLayer;
        return Physics.Raycast(origin, direction, distance, layer);
    }
}
