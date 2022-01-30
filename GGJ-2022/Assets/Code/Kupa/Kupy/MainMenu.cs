using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public bool _isMainMenu = true;
	public Button _continueButton;
	public Button _startButton;
	public Button _optionsButton;
	public Button _exitButton;
	public Image _tutorialObject;

	bool _isPaused = false;

	void Start()
	{
		_startButton.onClick.AddListener(StartGame);
		_optionsButton.onClick.AddListener(OpenOptions);
		_exitButton.onClick.AddListener(ExitGame);

		if (_isMainMenu)
        {
			ShowButtons();
        }
		else
		{
			_startButton.GetComponentInChildren<Text>().text = "Restart";
			_continueButton.onClick.AddListener(Continue);
			HideButtons();
		}
	}

	void ShowButtons()
    {
		_continueButton.gameObject.SetActive(!_isMainMenu);
		_startButton.gameObject.SetActive(true);
		_optionsButton.gameObject.SetActive(true);
		_exitButton.gameObject.SetActive(true);
		_tutorialObject.gameObject.SetActive(false);
	}

	void HideButtons()
    {
		_continueButton.gameObject.SetActive(false);
		_startButton.gameObject.SetActive(false);
		_optionsButton.gameObject.SetActive(false);
		_exitButton.gameObject.SetActive(false);
		_tutorialObject.gameObject.SetActive(false);
	}

    void Update()
    {
        if (!_isMainMenu && !_isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
			_isPaused = true;
			Time.timeScale = 0f;
			ShowButtons();
        }
		else if (!_isMainMenu && _isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
			Continue();
        }
	}

	void Continue()
    {
		_isPaused = false;
		Time.timeScale = 1f;
		HideButtons();
	}

    void StartGame()
	{
		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
	}

	void OpenOptions()
    {
		_tutorialObject.gameObject.SetActive(!_tutorialObject.gameObject.activeSelf);
    }

	void ExitGame()
    {
#if UNITY_EDITOR
		// Application.Quit() does not work in the editor so
		// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
		UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
	}
}
