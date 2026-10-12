using UnityEngine;

public class DialogueIcon : MonoBehaviour
{

    //The icon that goes with this text
    [SerializeField]
    private Sprite DoorInfoCharacterIcon;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DialogueManager.instance.SetDialogueSprite(DoorInfoCharacterIcon);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        DialogueManager.instance.ClearInfoText();
    }
}
