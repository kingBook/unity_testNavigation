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
        Debug.Log("Start");
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

                    var path = new NavMeshPath();
                    bool isFound = m_agent.CalculatePath(mousePointOnPlane.Value, path);
                    DrawPath(path.corners);
                }
            }
        } else {
            Debug.Log($"remainingDistance:{m_agent.remainingDistance} stoppingDistance:{m_agent.stoppingDistance} nextPosition:{m_agent.nextPosition}");

            // 旋转
            //Vector3 lookAt = m_agent.nextPosition;
            //lookAt.y = transform.position.y;
            //transform.LookAt(lookAt);

            // 判断行走完成
            if (m_agent.remainingDistance < 0.01f) {
                m_agent.isStopped = true;
            }
        }


    }

    private void DrawPath (Vector3[] path) {
        for (int i = 0; i < path.Length; i++) {
            //Debug.Log($"i:{i} {path[i]}");
            if (i < path.Length - 1) {
                Debug.DrawLine(path[i], path[i + 1],Color.red,2f);
            }
        }
    }
}
