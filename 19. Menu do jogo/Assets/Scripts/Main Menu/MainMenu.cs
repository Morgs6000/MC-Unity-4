using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {    
    [SerializeField] private Button singleplayer;
    [SerializeField] private Button quitGame;
    
    private void Start() {        
        singleplayer.onClick.AddListener(Singleplayer);
        quitGame.onClick.AddListener(QuitGame);
    }

    private void Update() {
        
    }

    private void Singleplayer() {
        SceneManager.LoadScene("Game");
    }

    private void QuitGame() {
        Application.Quit();
    }
}
