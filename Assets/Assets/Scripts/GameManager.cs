using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class GameManager : Singleton<GameManager>
{
	[SerializeField] GameObject titleUI;
	[SerializeField] GameObject startUI;
	[SerializeField] GameObject winUI;
	[SerializeField] GameObject gameoverUI;
	[SerializeField] TMP_Text speedUI;
	[SerializeField] TMP_Text timerUI;
	[SerializeField] Slider healthUI;

	[SerializeField] floatVariable health;
	[Header("Events")]
	[SerializeField] IntEvent scoreEvent;
	[SerializeField] VoidEvent gamestartEvent;
	[Header("RigidBody")]
	[SerializeField] Rigidbody rb;

	public enum State
	{
		TITLE,
		START_GAME,
		PLAY_GAME,
		GAME_OVER,
		WIN
	}

	public State state = State.TITLE;
	public float timer = 0;
	public float speed = 0;


	public float Speed
	{
		get { return rb.velocity.magnitude; }
		set
		{
			speed = value;

			speedUI.text = "speed: " + speed.ToString();
		}
	}

	public float Timer
	{
		get { return timer; }
		set
		{
			timer = value;
			timerUI.text = string.Format("{0:F1}", timer);
		}
	}

	public void OnEnable()
	{
		scoreEvent.Subscribe(OnAddPoints);
	}

	public void OnDisable()
	{
		scoreEvent.Unsubscribe(OnAddPoints);
	}

	void Start()
	{
		//
	}
	void Update()
	{
		switch (state)
		{
			case State.TITLE:
				titleUI.SetActive(true);
				startUI.SetActive(false);
				winUI.SetActive(false);
				gameoverUI.SetActive(false);
				Timer = 60;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
				titleUI.SetActive(false);
				startUI.SetActive(true);
				winUI.SetActive(false);
				gameoverUI.SetActive(false);
				Timer = 60;
				Speed = 0;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				gamestartEvent.RaiseEvent();
				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:
				titleUI.SetActive(false);
				startUI.SetActive(true);
				winUI.SetActive(false);
				gameoverUI.SetActive(false);
				Timer = Timer - Time.deltaTime;
				if (Timer <= 0)
				{
					state = State.GAME_OVER;
				}
				break;
			case State.GAME_OVER:
				titleUI.SetActive(false);
				startUI.SetActive(false);
				winUI.SetActive(false);
				gameoverUI.SetActive(true);
				break;
			case State.WIN:
				titleUI.SetActive(false);
				startUI.SetActive(false);
				winUI.SetActive(true);
				gameoverUI.SetActive(false);
				break;
			default:
				break;
		}
		healthUI.value = health.value / 100.0f;
	}




	public void OnStartGame()
	{
		state = State.START_GAME;
	}

	public void OnAddPoints(int points)
	{
		print(points);
	}

	public void OnPlayGame()
	{
		state = State.PLAY_GAME;
	}

	public void OnQuitGame()
	{
		Application.Quit();
	}
}
