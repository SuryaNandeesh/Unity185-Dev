using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ProjectileAmmo : Ammo
{
    [SerializeField] Action action;

    // Start is called before the first frame update
    private void Start()
    {
        if(action != null)
        {
            action.onEnter += OnInteractStart;
            action.onStay += OnInteractActive;
            action.onExit += OnInteractEnd;
        }

        if (ammoData.force != 0) GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * ammoData.force, ammoData.forceMode);
    }
}
