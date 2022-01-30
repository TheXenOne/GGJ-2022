using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayCanvas : MonoBehaviour
{
    public Text _failText;
    public Text _winText;
    public Button _mainMenuButton;

    HealthComponent _playerHealth;
    Chonkfactory _chonkies;

    public void HideMenu()
    {
        _failText.gameObject.SetActive(false);
        _winText.gameObject.SetActive(false);
        _mainMenuButton.gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        _failText.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);

        _playerHealth.gameObject.GetComponent<LandController>()._hasControl = false;
        _playerHealth.gameObject.GetComponent<VehicleController>()._hasControl = false;
    }

    public void ShowMenuYouChamp()
    {
        _winText.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);
    }

    void Awake()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
        _chonkies = GameObject.FindGameObjectWithTag("Player").GetComponent<Chonkfactory>();
        _playerHealth._deathDelegate = ShowMenu;
        _chonkies._winDelegate = ShowMenuYouChamp;
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
