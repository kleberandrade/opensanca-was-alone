using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    private Player[] m_Players;
    private CameraFollow m_CameraFollow;
    private int m_CurrentPlayer = 0;
    public bool m_LoadAndSave = false;

    private void Start()
    {
        m_CameraFollow = FindObjectOfType<CameraFollow>();
        m_Players = FindObjectsOfType<Player>();
        m_Players[m_CurrentPlayer].Enabled = true;
        m_CameraFollow.ChangeTarget(m_Players[m_CurrentPlayer].transform);

        if (m_LoadAndSave)
            Load();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            ChangePlayer();
            m_CameraFollow.ChangeTarget(m_Players[m_CurrentPlayer].transform);
        }

        if (Input.GetButtonDown("Fire3"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    private void ChangePlayer()
    {
        m_Players[m_CurrentPlayer].Enabled = false;
        m_CurrentPlayer = (++m_CurrentPlayer) % m_Players.Length;
        m_Players[m_CurrentPlayer].Enabled = true;
    }

    private void OnApplicationQuit()
    {
        if (m_LoadAndSave)
            Save();
    }

    private void Save()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            string json = JsonUtility.ToJson(m_Players[i].transform.position);
            PlayerPrefs.SetString("Player" + i, json);
        }
        PlayerPrefs.SetInt("CurrentPlayer", m_CurrentPlayer);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            if (PlayerPrefs.HasKey("Player" + i))
                m_Players[i].transform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("Player" + i));

            m_CurrentPlayer = PlayerPrefs.GetInt("CurrentPlayer");
            m_Players[0].Enabled = false;
            m_Players[m_CurrentPlayer].Enabled = true;
        }
    }
}
