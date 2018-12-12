using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour {

    static GUI m_Instance;
    Camera m_Camera;
	void Awake () {
        m_Instance = this;
        m_Camera = Camera.main;
	}

    public static GUI GetInstance()
    {
        return m_Instance;
    }

    IBombSpawner m_BombSpawner;

    public void SetBombSpawner(IBombSpawner bombSpawner) {
        m_BombSpawner = bombSpawner;
    }

    public void TryToCreateBomb()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            m_BombSpawner.SpawnBomb(hit.point);
        }
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0))
            TryToCreateBomb();
    }
}
