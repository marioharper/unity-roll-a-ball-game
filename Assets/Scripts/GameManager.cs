using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public Text scoreText;
	public Text winText;
	public Text pointText;

	private int score = 0;
	private int pointsToDisplay = 0;
	private IEnumerator displayPointsCoroutine;

	/* PRIVATE */
	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance == this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
		InitGame ();
	}

	void InitGame () {
		scoreText.text = "Score: ";
		winText.text = "";
		pointText.text = "";
	}

	void UpdateCountText() {
		scoreText.text = "Score: " + score.ToString ();

		if (score >= 12) {
			winText.text = "You Win!";
		}
	}

	IEnumerator DisplayPoints (int points) {
		if (points > 0 && pointsToDisplay > 0 || points < 0 && pointsToDisplay < 0) {
			pointsToDisplay += points;
		} else {
			pointsToDisplay = points;
		}

		pointText.text = pointsToDisplay.ToString();

		if (pointsToDisplay > 0) {
			pointText.color = Color.green;
		} else {
			pointText.color = Color.red;
		}
		
		yield return new WaitForSeconds(2);

		pointText.text = "";
		pointsToDisplay = 0;
	}
		
	/* PUBLIC */
	public void AddPoints(int points) {
		if (displayPointsCoroutine != null) {
			StopCoroutine (displayPointsCoroutine);
		}

		displayPointsCoroutine = DisplayPoints (points);

		StartCoroutine(displayPointsCoroutine);

		score += points;	
		UpdateCountText ();
	}

	public int GetScore() {
		return score;
	}

}
