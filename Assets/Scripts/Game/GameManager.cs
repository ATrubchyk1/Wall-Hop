using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 1)] private float _pointSpawnProbability = 0.7f;
    [SerializeField] private PointController _pointController;
    [SerializeField] private ObstacleController _obstacleController;
    [SerializeField] private PlayerController _player;
    [SerializeField] private LevelMover _levelMover;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private BackgroundColorController _backgroundColorController;
    [Tooltip("Points required to change background color")]
    [SerializeField] private int _colorChangePeriodInPoints = 5;
    [SerializeField] private int _difficultyIncreasePeriodInPoints = 10;
    [SerializeField] private float _sceneChangeDelay = 1f;
    
    private void Awake()
    {
        _obstacleController.ObstacleChangedPosition += OnObstacleChangedPosition;
        _pointController.RewardAdded += _scoreController.AddScore;
        _player.PlayerDied += OnPlayerDied;
        //_scoreController.ScoreChanged += _scoreView.UpdateScoreLabel;
        _scoreController.ScoreChanged += OnScoreChanged;
    }
    
    private IEnumerator LoadGameOverSceneWithDelay()
    {
        // Слово "yield" используется для создания задержки в выполнении кода.
        // Метод "WaitForSeconds" приостанавливает выполнение текущей корутины (или метода) на указанное количество секунд.
        // Это позволяет создавать временные задержки в игре, например, перед переключением сцен.
        // Более подробную информацию о корутинах и "yield" можно найти на курсах или в ресурсах онлайн.
        yield return new WaitForSeconds(_sceneChangeDelay);
        SceneManager.LoadSceneAsync(GlobalConstants.GAME_OVER_SCENE);
    }
    
    private void OnScoreChanged(int score)
    {
        _scoreView.UpdateScoreLabel(score);
        if (score % _colorChangePeriodInPoints == 0)
        {
            _backgroundColorController.ChangeColor();
        }
        
        if (score % _difficultyIncreasePeriodInPoints == 0)
        {
            _levelMover.IncreaseSpeed();
        }
    }
    
    private void OnPlayerDied()
    {
        _levelMover.enabled = false;
        _obstacleController.DestroyAllObstacles();
        _pointController.DestroyAllPoints();
        
        StartCoroutine(LoadGameOverSceneWithDelay());
    }
    
    private void OnObstacleChangedPosition(Vector3 position)
    {
        var randomValue = Random.value;
        
        if (randomValue <= _pointSpawnProbability)
        {
            _pointController.SpawnPoint(position);
        }
    }
    
    
    
    private void OnDestroy()
    {
        _obstacleController.ObstacleChangedPosition -= OnObstacleChangedPosition;
        _player.PlayerDied -= OnPlayerDied;
        _pointController.RewardAdded -= _scoreController.AddScore;
        // _scoreController.ScoreChanged -= _scoreView.UpdateScoreLabel;
        _scoreController.ScoreChanged -= OnScoreChanged;
    }
}
