using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackEnemy : EnemyController
{
    private GameObject _goal;
    private float _defaultDistance;
    private GameUI _gameUI;
    public override void Init()
    {
        base.Init();
        
        SetDefaultDistance();
        //GameAudioManager.Instance.Play2DSoundLoop("BackEnemyWarning");
    }

    protected override void Run()
    {
        base.Run();
        Move();
        SetBackEnemyProgressBar();
        SetWarningSound();
    }

    private void Move()
    {
        Vector2 curPos = transform.position;
        Vector2 targetPos = Vector2.right * xIndex * speed * Time.deltaTime;
        transform.position = curPos + targetPos;
    }
    public override void Damaged()
    {
        _player.Dead();
    }

    private void SetWarningSound()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        float distance = Vector2.Distance(_player.transform.position, transform.position);
        float value = 1 - (distance / 70);
        if (value < 0)
            value = 0;

        //GameAudioManager.Instance.Set2DLoopSound(value);
    }

    private void SetDefaultDistance()
    {
        if (_goal == null)
            _goal = GameObject.FindGameObjectWithTag("Goal");
        if (_gameUI == null)
            _gameUI = UIManager.Instance.Get<GameUI>();
        _defaultDistance = Vector2.Distance(transform.position, _goal.transform.position);
    }

    private void SetBackEnemyProgressBar()
    {
        if (_goal == null)
            _goal = GameObject.FindGameObjectWithTag("Goal");
        if (_gameUI == null)
            _gameUI = UIManager.Instance.Get<GameUI>();
        float curdistance = Vector2.Distance(transform.position, _goal.transform.position);

        float value = 1 - (curdistance / _defaultDistance);
        if (value != 0 && _gameUI != null)
        {
            _gameUI.SetBackEnemyProGressBar(value);
        }
    }
}
