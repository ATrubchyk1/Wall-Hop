using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private ColorProvider _colorProvider;
    [SerializeField] private TextMeshProUGUI _currentScoreLabel;
    [SerializeField] private TextMeshProUGUI _bestScoreLabel;
    [SerializeField] private float _newBestScoreAnimationDuration = 0.3f;
    [SerializeField] private AudioSource _bestScoreChangedAudio;
    
    private void Awake()
    {
        Camera.main.backgroundColor = _colorProvider.CurrentColor;

        var currentScore = PlayerPrefs.GetInt(GlobalConstants.SCORE_PREFS_KEY);
        var bestScore = PlayerPrefs.GetInt(GlobalConstants.BEST_SCORE_PREFS_KEY);
        
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            ShowNewBestScoreAnimation();
            SaveNewBestScore(bestScore);
        }
        
        _currentScoreLabel.text = currentScore.ToString();
        _bestScoreLabel.text = $"BEST {bestScore.ToString()}";
    }
    
    private void ShowNewBestScoreAnimation()
    {
        _bestScoreLabel.transform.DOPunchScale(Vector3.one, _newBestScoreAnimationDuration, 0);
        _bestScoreChangedAudio.Play();
    }
    
    private void SaveNewBestScore(int bestScore)
    {
        PlayerPrefs.SetInt(GlobalConstants.BEST_SCORE_PREFS_KEY, bestScore);
        PlayerPrefs.Save();
    }
    
    [UsedImplicitly]
    public void RestartGame()
    {
        _colorProvider.CurrentColor = _colorProvider.GetRandomColor(_colorProvider.CurrentColor);
        SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
    }
    
    [UsedImplicitly]
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
