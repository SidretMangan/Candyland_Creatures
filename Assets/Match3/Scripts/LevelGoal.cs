using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelCounter
{
	Timer,
	Moves,
	None
}

// class is abstract, use a subclass and re-define the abstract methods
public abstract class LevelGoal : Singleton<LevelGoal>
{
	// the number of stars earned for this level
	public int scoreStars = 0;

	// minimum scores used to earn stars
	public int[] scoreGoals = new int[3] { 1000, 2000, 3000 };

	public int startingScore = 500;

	public LevelCounter levelCounter = LevelCounter.Moves;

	// seconds if levelcounter is time, moves if level counter is moves
	public int counterLimit = 30;

	int m_maxTime;

	public virtual void Start()
	{
		Init();

		if (levelCounter == LevelCounter.Timer)
		{
			m_maxTime = counterLimit;

			if (UIManager.Instance != null && UIManager.Instance.timer != null)
			{
				UIManager.Instance.timer.InitTimer(counterLimit);
			}
		}
	}

	public void Init()
	{

		ScoreManager.Instance.AddScore(startingScore);
		UIManager.Instance.scoreMeter.UpdateScoreMeter(ScoreManager.Instance.CurrentScore,
						scoreStars);

		// reset scoreStars
		scoreStars = 0;

		// doublecheck that scoreGoals are setup in increasing order
		for (int i = 1; i < scoreGoals.Length; i++)
		{
			if (scoreGoals[i] < scoreGoals[i - 1])
			{
				Debug.LogWarning("LEVELGOAL Setup score goals in increasing order!");
			}
		}
	}

	// return number of stars given a score value
	public int UpdateScore(int score)
	{
		for (int i = 0; i < scoreGoals.Length; i++)
		{
			if (score < scoreGoals[i])
			{
				return i;
			}
		}
		return scoreGoals.Length;

	}

	// set scoreStars based on current score
	public void UpdateScoreStars(int score)
	{
		scoreStars = UpdateScore(score);
	}

	// abstract methods to be re-defined in subclass
	public abstract bool IsWinner();
	public abstract bool IsGameOver();


	// public method to start the timer
	public void StartCountdown()
	{
		StartCoroutine(CountdownRoutine());
	}

	// decrement the timeLeft each second
	IEnumerator CountdownRoutine()
	{
		while (counterLimit > 0)
		{
			yield return new WaitForSeconds(1f);
			counterLimit--;

			if (UIManager.Instance != null && UIManager.Instance.timer != null)
			{
				UIManager.Instance.timer.UpdateTimer(counterLimit);
			}
		}
	}

	public void AddTime(int timeValue)
	{
		counterLimit += timeValue;
		counterLimit = Mathf.Clamp(counterLimit, 0, m_maxTime);

		if (UIManager.Instance != null && UIManager.Instance.timer != null)
		{
			UIManager.Instance.timer.UpdateTimer(counterLimit);
		}
	}

}
