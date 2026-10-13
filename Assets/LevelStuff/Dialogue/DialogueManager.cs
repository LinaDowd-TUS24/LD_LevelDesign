using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    // This is the TMP Pro text that contains the inputed text
    public TMP_Text infoText;

    //The string that goes into the coroutine
    private string textToDisplay;

    //The Coroutine that prints the text one letter at a time
    private Coroutine thePrintCoroutine;

    //How fast the text is printed
    [SerializeField]
    private float textSpeed = 0.03f;

    //The panel that the players info notes pop up in
    [SerializeField]
    private GameObject DialogueBox;


    //The Game Object that holds the sprite icon
    [SerializeField]
    private GameObject SpriteIconObject;

    //The Image Component that holds the sprite
    private Image SpriteImageComponent;

    // This is the sprite that goes into the dialogue box sprite
    private Sprite SpriteIcon;

    void Start()
    {
        DialogueBox.SetActive(false);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {

    }

    public void DisplayInfoText(string text)
    {
        //Turn on the dialogue box
        DialogueBox.SetActive(true);

        //Set the text string to the inputed text
        textToDisplay = text;

        //Start the text printer coroutine
        thePrintCoroutine = StartCoroutine(PrintText());
    }

    public void ClearInfoText()
    {
        //Stop the text printer coroutine
        if (thePrintCoroutine != null)
        {
            StopCoroutine(thePrintCoroutine);
        }

        //Clear the text
        this.infoText.text = "";

        //Turn off the dialogue box
        DialogueBox.SetActive(false);
    }

    private IEnumerator PrintText()
    {
        infoText.text = "";

        for (int i = 0; i < textToDisplay.Length; i++)
        {
            infoText.text += textToDisplay[i];

            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void SetDialogueSprite(Sprite IncomingDialogueSprite)
    {
        SpriteIcon = IncomingDialogueSprite;

        SpriteImageComponent = SpriteIconObject.GetComponent<Image>();

        SpriteImageComponent.sprite = SpriteIcon;

    }

    public void ClearDialogueSprite()
    {
        SpriteImageComponent.sprite = null;
    }
}
