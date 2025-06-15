using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;

    [Header("Scene Name")]
    [SerializeField] private string startSceneName;
    [Header("Sets")]
    [SerializeField] private Transform mainMenuSet;
    [SerializeField] private Transform mainMenuButtonSet;
    [SerializeField] private Transform gameOverSet;
    [Header("Buttons")]
    [SerializeField] private Transform startButton;
    [SerializeField] private Transform continueButton;
    [SerializeField] private Transform saveButton;

    bool mainMenuOpen = false;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startButton.gameObject.SetActive(PlayerPrefs.GetInt("gamestarted", 0) == 0);
        continueButton.gameObject.SetActive(PlayerPrefs.GetInt("gamestarted", 0) == 1);
    }

    private void OnDisable()
    {
        // PlayerPrefs.SetInt("gamestarted", 0);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            mainMenuOpen = !mainMenuOpen;
            mainMenuSet.gameObject.SetActive(mainMenuOpen);
            saveButton.gameObject.SetActive(mainMenuOpen);
        }
    }

    public void PlayButtonClicked() 
    {
        mainMenuOpen = false;
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == startSceneName)
        {
            mainMenuSet.gameObject.SetActive(false);
            return;
        }
        mainMenuSet.gameObject.SetActive(false);
        PlayerPrefs.SetInt("gamestarted", 1);
        SceneManager.LoadScene(startSceneName);
    }

    public void MainMenuButton() 
    {
        mainMenuSet.gameObject.SetActive(true);
        mainMenuButtonSet.gameObject.SetActive(true);
        gameOverSet.gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveButtonClicked() 
    {
        //save i burada yap
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            player.GetComponent<PlayerMovement>().SavePosition();
        }
    }

    public void ShowGameOver() 
    {
        mainMenuButtonSet.gameObject.SetActive(false);
        mainMenuSet.gameObject.SetActive(true);
        gameOverSet.gameObject.SetActive(true);
    }

    public void RestartGame() 
    {
        mainMenuSet.gameObject.SetActive(false);
        gameOverSet.gameObject.SetActive(false);
        mainMenuButtonSet.gameObject.SetActive(true);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void CloseGame() 
    {
        PlayerPrefs.SetInt("gamestarted", 0);
        Application.Quit();
    }
}
