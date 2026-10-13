using UnityEngine;



//Put this on a character or thing the player can talk to. It needs a collider so the player's talk raycast can hit it.
public class DialogueNPC : MonoBehaviour
{

    [SerializeField]
    private DialogueNode _firstNode;

    public DialogueNode FirstNode 
    {  
        get { return _firstNode; } 
    }

}
