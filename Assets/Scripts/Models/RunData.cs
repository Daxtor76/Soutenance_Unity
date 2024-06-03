using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class RunData
{
	private float _runDuration;
	private float _distance;
	private int _enemiesKilled;
	private int _patounesCount;
    private int _totalScore;

    public RunData(float initTime)
    {
        _runDuration = initTime;
        _distance = 0.0f;
		_enemiesKilled = 0;
		_patounesCount = 0;
		_totalScore = 0;
	}

	public void SetGameDuration(float time) => _runDuration = time - _runDuration;
	public void SetDistance(float duration, float speed) => _distance = speed * duration;
	public void AddEnemiesKilled(int amount) => _enemiesKilled += amount;
	public void AddPatounes(int amount) => _patounesCount += amount;
	public void SetTotalScore(int amount) => _totalScore += amount;

	public int GetPatounesCount() => _patounesCount;
	public int GetTotalScore() => _totalScore;
    public int GetEnemiesKilled() => _enemiesKilled;
    public float GetDuration() => _runDuration;
	public float GetDistance() => _distance;
}
