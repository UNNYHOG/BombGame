using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IBombSpawner, IMonsterSpawner, ITransform {

    public float m_BombsRespawnHeight = 5;
    public float m_TestMonstersCount = 5;

    const string Prefab_Bomb = "Bomb";
    const string Prefab_Monster = "MobSiege";

    Transform m_Transform;

    public Transform GetTransform() {
        return m_Transform;
    }

    private void CreateBaseObject(Vector3 pos, string name) {
        IBufferable bufferable = SimpleBuffer.GetObject(name);
        Transform trm = bufferable.GetTransform();
        trm.SetParent(this.GetTransform());
        trm.localPosition = pos;
    }

    public void SpawnBomb(Vector3 pos) {
        pos.y = m_BombsRespawnHeight;
        CreateBaseObject(pos, Prefab_Bomb);
    }

    public void SpawnMonster(Vector3 pos)
    {
        CreateBaseObject(pos, Prefab_Monster);
    }

    void Awake() {
        m_Transform = transform;
    }

    void SpawnRandomMonstersForTests() {
        for (int i = 0; i < m_TestMonstersCount; i++) {
            Vector3 pos = new Vector3(Random.Range(-9, 9), 0, Random.Range(-9, 9));
            SpawnMonster(pos);
        }
    }

    void Start() {
        SimpleBuffer.Initialize();
        GUI.GetInstance().SetBombSpawner(this);
        SpawnRandomMonstersForTests();
    }
}
