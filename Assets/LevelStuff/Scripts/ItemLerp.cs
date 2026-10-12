using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemLerp : MonoBehaviour
{
    public Transform Item;
    public Transform handMarker;
    public Transform holsterMarker;

    public float lerpSpeed = 5f;
    public bool itemInHand = false;

    private Animator theAnimator;
    private bool holding;
    private bool use;

    // Start is called before the first frame update
    void Start()
    {
        holding = false;
        use = false;
        theAnimator = GetComponent<Animator>();
    }

    public void OnAttack(InputValue value)
    {
        Debug.Log("Use");
        use = !use;
        theAnimator.SetBool("Use", true);
    }

    public void OnEquip(InputValue value)
    {
        holding = !holding;
        theAnimator.SetBool("Holding", holding);

        //If the item is already in hand, we change it's parent to the sheathMarker
        if (itemInHand)
        {
            itemInHand = false;
            Item.parent = holsterMarker;
        }
        //If it's not, we change it's parent to the handMarker
        else
        {
            itemInHand = true;
            Item.parent = handMarker;
        }
    }

    public void EquipReset()
    {
        holding = false;
        theAnimator.SetBool("Holding", false);

        //If the item is already in hand, we change it's parent to the sheathMarker
        if (itemInHand)
        {
            itemInHand = false;
            Item.parent = holsterMarker;
        }
        //If it's not, we change it's parent to the handMarker
        else
        {
            itemInHand = true;
            Item.parent = handMarker;
        }
    }

    public void Draw()
    {
        //If the item is already in hand, we change it's parent to the sheathMarker
        if (itemInHand)
        {
            itemInHand = false;
            Item.parent = holsterMarker;
        }
        //If it's not, we change it's parent to the handMarker
        else
        {
            itemInHand = true;
            Item.parent = handMarker;
        }
    }

    void Update()
    {


        //Here we Lerp item's position and rotation with it's parent slot to be sure our item will always match the slot's position
        if (Item.parent != null)
        {
            if ((Item.position - Item.parent.position).sqrMagnitude > 0.0001f)
            {
                Item.position = Vector3.Lerp(Item.position, Item.parent.position, Time.deltaTime * lerpSpeed);
                Item.rotation = Quaternion.Lerp(Item.rotation, Item.parent.rotation, Time.deltaTime * lerpSpeed);
            }
        }

    }

    private void LateUpdate()
    {
        use = false;
    }

}

