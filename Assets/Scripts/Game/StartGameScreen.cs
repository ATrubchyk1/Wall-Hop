using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScreen : MonoBehaviour
{
    [SerializeField] private ColorProvider _colorProvider;
    [SerializeField] private TextMeshProUGUI _bestScoreLabel;
    
    private void Start()
    {
        var randomColor = _colorProvider.GetRandomColor();
        _colorProvider.CurrentColor = randomColor;
        Camera.main.backgroundColor = randomColor;
        
        var bestScore = PlayerPrefs.GetInt(GlobalConstants.BEST_SCORE_PREFS_KEY, 0);
        _bestScoreLabel.text = $"BEST {bestScore.ToString()}";
    }
    
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
    }
}
