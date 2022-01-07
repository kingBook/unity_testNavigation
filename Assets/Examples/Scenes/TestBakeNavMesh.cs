using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TestBakeNavMesh : MonoBehaviour {

    [Tooltip("使用的组 ID ")]public int useGroupId = 1;
    public GameObject[] group1;
    public GameObject[] group2;

    private NavMeshSurface m_navMeshSurface;

    private void Start () {
        m_navMeshSurface = GetComponent<NavMeshSurface>();

        // 激活使用的对象组的对象
        GameObject[] group = useGroupId == 1 ? group1 : group2;
        for (int i = 0; i < m_navMeshSurface.gameObject.transform.childCount; i++) {
            GameObject child= m_navMeshSurface.gameObject.transform.GetChild(i).gameObject;
             child.SetActive(System.Array.IndexOf(group, child) > -1);
        }

        // 使用附加 NavMesh Surface 对象的所有子对象
        m_navMeshSurface.collectObjects = CollectObjects.Children;

        // 烘培导航网格
        m_navMeshSurface.BuildNavMesh();
    }

    
}
