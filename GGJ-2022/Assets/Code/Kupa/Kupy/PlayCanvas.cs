using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayCanvas : MonoBehaviour
{
    public Text _failText;
    public Button _mainMenuButton;

    HealthComponent _playerHealth;

    public void HideMenu()
    {
        _failText.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        _failText.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);
    }

    void Awake()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
        _playerHealth._deathDelegate = ShowMenu;
    }

    void Start()
    {
        _mainMenuButton.onClick.AddListener(MainMenuButtonPressed);
        HideMenu();
    }

    void MainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
