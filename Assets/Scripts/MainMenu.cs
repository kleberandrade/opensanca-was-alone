using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour 
{
    private AudioSource m_AudioSource;

	private void Start()
	{
		Button playButton = transform.GetChild (0).GetComponent<Button> ();
		playButton.onClick.AddListener (delegate { PlayGame (); });
        Button continueButton = transform.GetChild(1).GetComponent<Button>();
        continueButton.onClick.AddListener(delegate { Exit(); });
        Button exitButton = transform.GetChild (1).GetComponent<Button> ();
		exitButton.onClick.AddListener (delegate { Exit (); });

        continueButton.interactable = PlayerPrefs.HasKey(GlobalKeys.CurrentPlayerKey);
	}

	public void PlayGame()
	{
        m_AudioSource.Play();
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene ("Level 1");
	}

    public void ContinueGame()
    {
        m_AudioSource.Play();
        SceneManager.LoadScene("Level 1");
    }

	public void Exit()
	{
        m_AudioSource.Play();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
