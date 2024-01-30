using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{

	private void OnCollisionEnter(Collision collision)
	{
		GameManager.Instance.state = GameManager.State.WIN;
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.state = GameManager.State.WIN;
        }
	}
}
