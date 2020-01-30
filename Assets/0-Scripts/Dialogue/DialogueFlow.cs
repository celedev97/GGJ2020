using Dialogue;
using Dialogue.Text;
using Dialogue.Logical;
using Dialogue.Functional;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueFlow : MonoBehaviour
{
    public DialogueGraph dialogue;
    public GameObject decorations;
    private TextInputManager textInputManager;
    public Dictionary<string, object> variables = new Dictionary<string, object>();

    // Start is called before the first frame update
    void Start()
    {
        dialogue.flow = this;
        if (!textInputManager){
            textInputManager = GetComponent<TextInputManager>();
        }

        dialogue.gameText = textInputManager.gameText;
        dialogue.flow = this;

        dialogue.Restart();
        Flow();
    }
    
    public void Flow(string input = null)
    {
        #region START
        if (dialogue.current.GetType() == typeof(Start))
        {
            dialogue.current.Trigger();
            Flow();
        }
        #endregion
        #region TEXT
        else if (dialogue.current.GetType().IsSubclassOf(typeof(Text)) || dialogue.current.GetType() == typeof(Text))
        {
            Text message = this.dialogue.current as Text;
            #region PREPARAZIONE DEL MESSAGGIO
            if (input == null)
            {
                //There is no input, it's the first time this dialog get flowed on
                textInputManager.inputNone();
                if (message.dont_clear) {
                    textInputManager.appendBaseText(message.text);
                } else {
                    textInputManager.setBaseText(message.text);
                }
                

                #region CHOICE
                if (message.GetType() == typeof(Choice))
                {
                    Choice choice = message as Choice;
                    if (choice.answers.Count > 0)
                    {
                        string text = "\r\n";
                        for (int i = 0; i < choice.answers.Count; i++)
                        {
                            text += "\r\n" + (i + 1) + ". " + choice.answers[i].text;
                        }
                        text += "\r\n\r\n" + choice.afterText;

                        textInputManager.inputNumeric(choice.answers.Count);
                        textInputManager.appendBaseText(text);
                    }
                }
                #endregion
                #region INPUT
                else if (message.GetType() == typeof(Dialogue.Text.Input))
                {
                    textInputManager.inputText();
                }
                #endregion
                #region YES/NO
                else if (dialogue.current.GetType() == typeof(YesNo))
                {
                    textInputManager.inputText();
                }
                #endregion
            }
            #endregion
            #region INTERPRETAZIONE DELLA RISPOSTA
            else
            {
                if (dialogue.current.GetType() == typeof(YesNo))
                {
                    //TODO: input può essere "", aiuto!
                    if (input.ToUpper()[0] == 'S')
                    {
                        (message as YesNo).Trigger(true);
                    }
                    else if (input.ToUpper()[0] == 'N')
                    {
                        (message as YesNo).Trigger(false);
                    }
                    Flow();
                }
                else
                {
                    message.Trigger(input == "" ? "-1" : input);
                    Flow();
                }
            }
            #endregion
        }
        #endregion
        #region EVENT
        else if (dialogue.current.GetType() == typeof(Dialogue.Functional.Event))
        {
            dialogue.current.Trigger();
            Flow();
        }
        #endregion
    }
}
