using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LevelGoalAnimal : LevelGoal
{
	public enum AnimalType
	{
		Red,
		Green,
		Blue
	}

	public AnimalType animalType;

	[Header("Multipliers")]
	public float positiveMultiplier = 1.0f;
	public float neutralMultiplier = 0.5f;
	public float negativeMultiplier = -0.5f;

	public void UpdateGoals(GamePiece pieceToCheck)
	{
		if (pieceToCheck != null)
		{
			switch (animalType)
			{
				case AnimalType.Red:
					switch (pieceToCheck.matchValue)
					{
						case MatchValue.Red:
							ScoreManager.Instance.AddScore((int)(positiveMultiplier * pieceToCheck.scoreValue));
							break;
						case MatchValue.Green:
							ScoreManager.Instance.AddScore((int)(neutralMultiplier * pieceToCheck.scoreValue));
							break;
						case MatchValue.Blue:
							ScoreManager.Instance.AddScore((int)(negativeMultiplier * pieceToCheck.scoreValue));
							break;
					}
					break;
				case AnimalType.Green:
					switch (pieceToCheck.matchValue)
					{
						case MatchValue.Red:
							ScoreManager.Instance.AddScore((int)(neutralMultiplier * pieceToCheck.scoreValue));
							break;
						case MatchValue.Green:
							ScoreManager.Instance.AddScore((int)(positiveMultiplier * pieceToCheck.scoreValue));
							break;
						case MatchValue.Blue:
							ScoreManager.Instance.AddScore((int)(negativeMultiplier * pieceToCheck.scoreValue));
							break;
					}
					break;
				case AnimalType.Blue:
					switch (pieceToCheck.matchValue)
					{
						case MatchValue.Red:
							ScoreManager.Instance.AddScore((int)(negativeMultiplier * pieceToCheck.scoreValue));
							break;
						case MatchValue.Green:
							ScoreManager.Instance.AddScore((int)(neutralMultiplier * pieceToCheck.scoreValue));
							break;
						case MatchValue.Blue:
							ScoreManager.Instance.AddScore((int)(positiveMultiplier * pieceToCheck.scoreValue));
							break;
					}
					break;
			}
		}

		UpdateUI();
	}

	public void UpdateUI()
	{
		if (UIManager.Instance != null)
		{
			UIManager.Instance.UpdateCollectionGoalLayout();
		}
	}

	public override bool IsGameOver()
	{
		if (ScoreManager.Instance != null)
		{
			int maxScore = scoreGoals[scoreGoals.Length - 1];
			if (ScoreManager.Instance.CurrentScore >= maxScore)
			{
				return true;
			}
		}
		if (levelCounter == LevelCounter.Timer)
		{
			return (timeLeft <= 0);
		}
		else
		{
			return (movesLeft <= 0);
		}
	}

	public override bool IsWinner()
	{
		if (ScoreManager.Instance != null)
		{
			return (ScoreManager.Instance.CurrentScore >= scoreGoals[0]);
		}
		return false;
	}
}
