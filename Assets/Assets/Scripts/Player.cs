using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] TMP_Text scoreText;
	[SerializeField] floatVariable health;
	[SerializeField] PhysicsCharacterController characterController;
	[Header("Events")]
	[SerializeField] IntEvent scoreEvent = default;
	[SerializeField] VoidEvent gamestartEvent = default;

	private int score = 0;
    private Transform platformTransform;
    private Vector3 offset;
    private bool isAttached = false;

    public int Score 
	{ 
		get { return score; } 
		set { 
			score = value; 
			scoreText.text = score.ToString();
			scoreEvent.RaiseEvent(score);
		} 
	}

    private void OnEnable()
    {
		gamestartEvent.Subscribe(OnStartGame);
    }

    public void AddPoints(int points)
	{
		Score += points;
	}

    public void Start()
    {
		health.value = 100f;
    }

	void OnStartGame()
	{
		characterController.enabled = true;
	}

    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isAttached = true;
            platformTransform = collision.transform;
            offset = transform.position - platformTransform.position;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (isAttached)
        {
            transform.position = platformTransform.position + offset;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isAttached = false;
        }
    }
}
