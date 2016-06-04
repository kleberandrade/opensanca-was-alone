using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform m_Target;
    public Vector2 m_Smooth = Vector2.zero;
    private Transform m_Transform;
    private Vector3 m_Offset;
   
    private void Awake()
    {
        m_Transform = GetComponent<Transform>();
    }

    private void Start()
    {
        m_Offset = m_Transform.position - m_Target.position;
    }

    private void Update()
    {
        Vector3 position = m_Target.position + m_Offset;

        float x = Mathf.Lerp(m_Transform.position.x, position.x,
            Time.deltaTime * m_Smooth.x);

        float y = Mathf.Lerp(m_Transform.position.y, position.y,
            Time.deltaTime * m_Smooth.y);

        m_Transform.position = new Vector3(x, y, m_Transform.position.z);
    }

    public void ChangeTarget(Transform target)
    {
        m_Target = target;
    }
    
}
