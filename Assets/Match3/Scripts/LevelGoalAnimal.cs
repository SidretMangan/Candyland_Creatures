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
		if (ScoreManager.Instance.CurrentScore <= 0 || ScoreManager.Instance.CurrentScore >= scoreGoals[^1]) return;

		if (piece != null)
		{
			int calcScore = (piece.scoreValue * multiplier + bonus);
			switch (animal.animalType)
			{
				case Animal.AnimalType.Red:
					switch (piece.matchValue)
					{
						case MatchValue.Red:
							PositiveScore(calcScore);
							break;
						case MatchValue.Green:
							NeutralScore(calcScore);
							break;
						case MatchValue.Blue:
							NegativeScore(calcScore);
							break;
					}
					break;
				case Animal.AnimalType.Green:
					switch (piece.matchValue)
					{
						case MatchValue.Red:
							NeutralScore(calcScore);
							break;
						case MatchValue.Green:
							PositiveScore(calcScore);
							break;
						case MatchValue.Blue:
							NegativeScore(calcScore);
							break;
					}
					break;
				case Animal.AnimalType.Blue:
					switch (piece.matchValue)
					{
						case MatchValue.Red:
							NegativeScore(calcScore);
							break;
						case MatchValue.Green:
							NeutralScore(calcScore);
							break;
						case MatchValue.Blue:
							PositiveScore(calcScore);
							break;
					}
					break;
			}
		}
	}

	public void PositiveScore(float score)
	{
		ScoreManager.Instance.AddScore((int)(animal.positive * score));
		animal.animation.TriggerHappy();
	}

	public void NeutralScore(float score)
	{
		ScoreManager.Instance.AddScore((int)(animal.neutral * score));
		animal.animation.TriggerEating();
	}

	public void NegativeScore( float score)
	{
		ScoreManager.Instance.AddScore((int)(animal.negative * score));
		animal.animation.TriggerAngry();
	}

	public void UpdateUI()
	{
		if (UIManager.Instance != null)
		{
			UIManager.Instance.UpdateCollectionGoalLayout();
		}
		
	}

	public void InitializeAnimal()
	{
		if (animal.animation)
		{
			animal.animation.transform.localScale = Camera.main.orthographicSize * Vector3.one / 16f;
			animal.animation.transform.position = 
				Vector3.Scale(Camera.main.transform.position, new(1f, 1f, 0f)) 
				+ Vector3.up * (animal.animation.GetComponent<SpriteRenderer>().bounds.size.y / 2f);
			animal.animation.gameObject.SetActive(true);
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
			return (counterLimit <= 0 || ScoreManager.Instance.CurrentScore <= 0f);
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

	public AnimationsTrigger animation;
}
