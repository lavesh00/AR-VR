using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject main_menu;
    [SerializeField] private GameObject instructions;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInstructions()
    {
        this.main_menu.SetActive(false);
        this.instructions.SetActive(true);
    }

    public void ShowMainMenu()
    {
        this.instructions.SetActive(false);
        this.main_menu.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
