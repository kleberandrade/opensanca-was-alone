using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float m_Speed;
    public bool m_IsGrounded;
    public float m_JumpForce;

    private Rigidbody2D m_Rigidbody;
    private AudioSource m_AudioSource;
    private Transform m_Transform;
    private float m_Horizontal;
    private bool m_Jump;
    private Vector2 m_Movement;
    private Vector3 m_StartPosition;

    public bool Enabled { get; set; }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Transform = GetComponent<Transform>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_StartPosition = m_Transform.position;
    }

    private void Update()
    {
        if (!Enabled) return;
#if UNITY_ANDROID && !UNITY_EDITOR
        m_Horizontal = Mathf.Clamp(Input.acceleration.x, -1.0f, 1.0f);
#else
        m_Horizontal = Input.GetAxis("Horizontal");
#endif

        m_Jump = Input.GetButtonDown("Fire1");
        if (m_Jump && m_IsGrounded)
        {
            m_Rigidbody.AddForce(Vector2.up * m_JumpForce);
            m_AudioSource.Play();
        }

        m_Movement = Vector2.right * m_Horizontal * m_Speed * Time.deltaTime;

        m_Transform.Translate(m_Movement);
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("DeadZone"))
            m_Transform.position = m_StartPosition;
    }

    private void OnTriggerStay2D(Collider2D hit)
    {
        m_IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        m_IsGrounded = false;
    }
}
