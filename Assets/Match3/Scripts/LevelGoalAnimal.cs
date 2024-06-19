using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class LevelGoalAnimal : LevelGoal
{
	public Animal animal;

	public void UpdateGoals(GamePiece pieceToCheck)
	{
		if (pieceToCheck != null)
		{
			switch (animal.animalType)
			{
				case Animal.AnimalType.Red:
					switch (pieceToCheck.matchValue)
					{
						case MatchValue.Red:
							ScoreManager.Instance.AddScore((int)(animal.positive * pieceToCheck.scoreValue));
							break;
						case MatchValue.Green:
							ScoreManager.Instance.AddScore((int)(animal.neutral * pieceToCheck.scoreValue));
							break;
						case MatchValue.Blue:
							ScoreManager.Instance.AddScore((int)(animal.negative * pieceToCheck.scoreValue));
							break;
					}
					break;
				case Animal.AnimalType.Green:
					switch (pieceToCheck.matchValue)
					{
						case MatchValue.Red:
							ScoreManager.Instance.AddScore((int)(animal.neutral * pieceToCheck.scoreValue));
							break;
						case MatchValue.Green:
							ScoreManager.Instance.AddScore((int)(animal.positive * pieceToCheck.scoreValue));
							break;
						case MatchValue.Blue:
							ScoreManager.Instance.AddScore((int)(animal.negative * pieceToCheck.scoreValue));
							break;
					}
					break;
				case Animal.AnimalType.Blue:
					switch (pieceToCheck.matchValue)
					{
						case MatchValue.Red:
							ScoreManager.Instance.AddScore((int)(animal.negative * pieceToCheck.scoreValue));
							break;
						case MatchValue.Green:
							ScoreManager.Instance.AddScore((int)(animal.neutral * pieceToCheck.scoreValue));
							break;
						case MatchValue.Blue:
							ScoreManager.Instance.AddScore((int)(animal.positive * pieceToCheck.scoreValue));
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
	public float negative = -0.5f;
}
