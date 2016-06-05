using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private Player[] m_Players;
    private CameraFollow m_CameraFollow;
    private int m_CurrentPlayer = 0;

    private void Start()
    {
        m_CameraFollow = FindObjectOfType<CameraFollow>();
        m_Players = FindObjectsOfType<Player>();
        m_Players[m_CurrentPlayer].Enabled = true;
        m_CameraFollow.ChangeTarget(m_Players[m_CurrentPlayer].transform);

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
            SceneManager.LoadScene("Menu");
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
        Save();
    }

    private void Save()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            string json = JsonUtility.ToJson(m_Players[i].transform.position);
            PlayerPrefs.SetString(GlobalKeys.PlayerKey+ i, json);
        }
        PlayerPrefs.SetInt(GlobalKeys.CurrentPlayerKey, m_CurrentPlayer);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        for (int i = 0; i < m_Players.Length; i++)
        {
            if (PlayerPrefs.HasKey(GlobalKeys.PlayerKey + i))
            {
                string json = PlayerPrefs.GetString(GlobalKeys.PlayerKey + i);
                Vector3 position = JsonUtility.FromJson<Vector3>(json);
                m_Players[i].transform.position = position;
            }

            if (PlayerPrefs.HasKey(GlobalKeys.CurrentPlayerKey))
            {
                m_CurrentPlayer = PlayerPrefs.GetInt(GlobalKeys.CurrentPlayerKey);
            }

            m_Players[0].Enabled = false;
            m_Players[m_CurrentPlayer].Enabled = true;
        }
    }
}
