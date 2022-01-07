using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestBakeOnPlaying : MonoBehaviour {

    [SerializeField] private NavMeshAgent m_agent;

    private NavMeshSurface m_navMeshSurface;

    private void Start () {
        m_navMeshSurface = GetComponent<NavMeshSurface>();


        // 使用附加 NavMesh Surface 对象的所有子对象
        m_navMeshSurface.collectObjects = CollectObjects.Children;

        // 烘培导航网格
        m_navMeshSurface.BuildNavMesh();

        // 烘焙完成才激活 NavMeshAgent
        m_agent.gameObject.SetActive(true);
    }
}
