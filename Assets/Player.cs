#pragma warning disable 0649

using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

    private NavMeshAgent m_agent;

    private Vector3? GetMousePointOnPlane () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] raycastHits = new RaycastHit[32];
        int count = Physics.RaycastNonAlloc(ray, raycastHits);

        for (int i = 0; i < count; i++) {
            RaycastHit raycastHit = raycastHits[i];
            if (raycastHit.collider.gameObject.name == "Plane") {
                return raycastHit.point;
            }
        }
        return null;
    }

    private void Start () {
        m_agent = GetComponent<NavMeshAgent>();

        m_agent.isStopped = true;
    }

    private void Update () {
        if (m_agent.isStopped) {
            // 停止时，检测鼠标点击，输入行走的目标点
            if (Input.GetMouseButtonDown(0)) {
                Vector3? mousePointOnPlane = GetMousePointOnPlane();
                if (mousePointOnPlane != null) {
                    m_agent.isStopped = false;
                    m_agent.updateRotation = true;
                    m_agent.SetDestination(mousePointOnPlane.Value);
                }
            }
        } else {
            Debug.Log($"remainingDistance:{m_agent.remainingDistance} stoppingDistance:{m_agent.stoppingDistance} nextPosition:{m_agent.nextPosition}");

            // 旋转
            /*Vector3 lookAt = m_agent.nextPosition;
            lookAt.y = transform.position.y;
            transform.LookAt(m_agent.destination);*/
            
            // 判断行走完成
            if (m_agent.remainingDistance < 0.01f) {
                m_agent.isStopped = true;
            }
        }


    }
}
