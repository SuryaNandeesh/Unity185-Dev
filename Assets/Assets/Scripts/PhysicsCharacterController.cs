using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsCharacterController : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField][Range(1, 50)] float maxForce = 30;
	[SerializeField][Range(1, 10)] float jumpForce = 5;
	[SerializeField][Range(1, 10)] float dashForce = 10;
    [SerializeField] Transform view;
	[Header("Collision")]
	[SerializeField][Range(0, 5)] float rayLength = 1;
	[SerializeField] LayerMask groundLayerMask;
	[Header("Field Of View")]
	[SerializeField] private CinemachineVirtualCamera virtualCamera;
	[SerializeField] bool dynamicFOV;

	Rigidbody rb;
	Vector3 force = Vector3.zero;
    private bool canDash = true;
    public float dashCooldown = 0.2f; // Set the cooldown duration in second
	public float fallOff = -15f;


    void Start()
	{
		rb = GetComponent<Rigidbody>();
	}


	void Update()
	{
		Vector3 direction = Vector3.zero;

		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");

		Quaternion yrotation = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
		force = yrotation * direction * maxForce;

		Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
		if (Input.GetButtonDown("Jump") && CheckGround())
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
		DynamicFOV();

		if (transform.position.y < fallOff)
		{
			rb.position = new Vector3(0, 0, 0.5f);
		}
	}

	private void FixedUpdate()
	{
		rb.AddForce(force, ForceMode.Force);
	}

	private bool CheckGround()
	{
		return Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayerMask);
	}

	private void DynamicFOV() // also dash mechanic
	{
        
		if(canDash && Input.GetKey(KeyCode.LeftShift) && dynamicFOV == true) 
		{
			virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, 100, 10f * Time.deltaTime);
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            rb.velocity = inputDirection * Mathf.Lerp(30f, 90f, dashForce / 1000f);

            // Disable the dash ability and start the cooldown timer
            canDash = false;
            StartCoroutine(EnableDashAfterCooldown());
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, 60, 10f * Time.deltaTime);
        }
    }

    IEnumerator EnableDashAfterCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true; // Enable the dash ability after the cooldown period
    }
}
