using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimAndShoot : MonoBehaviour
{
    public Transform weapon;
    public Transform handMarker;
    public Transform holsterMarker;

    public float lerpSpeed = 5f;
    public bool weaponInHand = false;

    private Animator theAnimator;
    private bool aim;
    private bool shoot;

    // Start is called before the first frame update
    void Start()
    {
        aim = false;
        shoot = false;
        theAnimator = GetComponent<Animator>();
    }

    public void OnAim(InputValue value)
    {
        Debug.Log("Aim");
        aim = !aim;
        theAnimator.SetBool("Aim", aim);
    }

    public void OnShoot(InputValue value)
    {
        shoot = !shoot;
        theAnimator.SetBool("Shoot", shoot);
    }

    public void Draw()
    {
        //If the weapon is already in hand, we change it's parent to the sheathMarker
        if (weaponInHand)
        {
            weaponInHand = false;
            weapon.parent = holsterMarker;
        }
        //If it's not, we change it's parent to the handMarker
        else
        {
            weaponInHand = true;
            weapon.parent = handMarker;
        }
    }

    void Update()
    {


        //Here we Lerp weapon's position and rotation with it's parent slot to be sure our weapon will always match the slot's position
        if (weapon.parent != null)
        {
            if ((weapon.position - weapon.parent.position).sqrMagnitude > 0.0001f)
            {
                weapon.position = Vector3.Lerp(weapon.position, weapon.parent.position, Time.deltaTime * lerpSpeed);
                weapon.rotation = Quaternion.Lerp(weapon.rotation, weapon.parent.rotation, Time.deltaTime * lerpSpeed);
            }
        }

    }

    private void LateUpdate()
    {
        shoot = false;
    }

}
