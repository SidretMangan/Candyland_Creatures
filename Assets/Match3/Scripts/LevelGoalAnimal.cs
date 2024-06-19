using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LevelGoalAnimal : LevelGoal
{
	public Animal animal;

	public void UpdateGoals(GamePiece pieceToCheck)
	{
		

		UpdateUI();
	}

	public void AnimalScorePoints(GamePiece piece, int multiplier = 1, int bonus = 0)
	{
		if (piece != null)
		{
			int calcScore = (piece.scoreValue * multiplier + bonus);
			switch (animal.animalType)
			{
				case Animal.AnimalType.Red:
					switch (piece.matchValue)
					{
						case MatchValue.Red:
							ScoreManager.Instance.AddScore((int)(animal.positive * calcScore));
							break;
						case MatchValue.Green:
							ScoreManager.Instance.AddScore((int)(animal.neutral * calcScore));
							break;
						case MatchValue.Blue:
							ScoreManager.Instance.AddScore((int)(animal.negative * calcScore));
							break;
					}
					break;
				case Animal.AnimalType.Green:
					switch (piece.matchValue)
					{
						case MatchValue.Red:
							ScoreManager.Instance.AddScore((int)(animal.neutral * calcScore));
							break;
						case MatchValue.Green:
							ScoreManager.Instance.AddScore((int)(animal.positive * calcScore));
							break;
						case MatchValue.Blue:
							ScoreManager.Instance.AddScore((int)(animal.negative * calcScore));
							break;
					}
					break;
				case Animal.AnimalType.Blue:
					switch (piece.matchValue)
					{
						case MatchValue.Red:
							ScoreManager.Instance.AddScore((int)(animal.negative * calcScore));
							break;
						case MatchValue.Green:
							ScoreManager.Instance.AddScore((int)(animal.neutral * calcScore));
							break;
						case MatchValue.Blue:
							ScoreManager.Instance.AddScore((int)(animal.positive * calcScore));
							break;
					}
					break;
			}
		}
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
			return (counterLimit <= 0);
		}
		else
		{
			return (counterLimit <= 0);
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

[System.Serializable]

public class Animal
{
	public enum AnimalType
	{
		Red,
		Green,
		Blue
	}

	public AnimalType animalType;

	[Header("Multipliers")]
	public float positive = 1.0f;
	public float neutral = 0.5f;
	public float negative = -1.0f;
}
