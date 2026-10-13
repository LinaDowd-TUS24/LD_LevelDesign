using UnityEngine;
using UnityEngine.InputSystem;


//Let the player talk to NPCs and objects by looking at them and pressing E (the Interact Action)
public class PlayerTalk : MonoBehaviour
{

    [SerializeField]
    private DialogueRunner _dialogueRunner;

    //How far away (in meters) the player can be from an object and still talk to it
    [SerializeField]
    private float _talkDistance = 3f;

    private Transform _cameraTransform;


    void Start()
    {
        //Camera.main finds the camera tagged as "MainCamera"
        _cameraTransform = Camera.main.transform;
    }

    public void OnInteract(InputValue value)
    {
        //PlayerInput calls this when the key goes down (started), is pressed (performed), and goes up (canceled). We only want to react once per press.
        if (value.isPressed)
        {
            if (_dialogueRunner.IsTalking)
            {
                _dialogueRunner.Advance();
            }
            else
            {
                TryToTalk();
            }
        }
    }

    private void TryToTalk()
    {
        //the ray starts at the camera and goes straight ahread, where the player is looking
        Vector3 rayStart = _cameraTransform.position;
        Vector3 rayDirection = _cameraTransform.forward;

        //Several checks in a row, so each one stops early if theres nobody to talk to.
        RaycastHit hit;
        if (Physics.Raycast(rayStart, rayDirection, out hit, _talkDistance) == false)
        {
            return;

        }

        DialogueNPC npc = hit.collider.GetComponent<DialogueNPC>();
        if (npc == null)
        {
            return;
        }

        if (npc.FirstNode == null)
        {
            Debug.LogWarning(npc.name + "has no First Node, so it has nothing to say.");
            return;
        }

        _dialogueRunner.StartConversation(npc.FirstNode);

    }
}
