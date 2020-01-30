using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class TextInputManager : MonoBehaviour
{
    [HideInInspector]public DialogueFlow flow;
    public Text gameText;

    public float timeStep = .1f;
    public float caretTimeStep = .5f;

    #region Private Vars
    #region private string baseText;
    private string _baseText;
    private string baseText {
        get {
            return _baseText;
        }
        set {
            _baseText = replace_variables(value);
            currentText = _baseText.Substring(0, charIndex);
        }
    }
    #endregion

    private string currentText;
    private int charIndex = 0;

    private float timer = 0;
    private float caretTimer = 0;

    private bool caret = false;

    //INPUT VARS
    string input;

    private bool inputNumbers = false;
    private int maxInput = int.MaxValue;
    private bool inputCharacters = false;

    //NUMBERS
    private KeyCode[] numbers = {
         KeyCode.Alpha0,
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };
    #endregion

    KeyCode[] alphanumeric = new KeyCode[] {
        KeyCode.A,
        KeyCode.B,
        KeyCode.C,
        KeyCode.D,
        KeyCode.E,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.I,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.M,
        KeyCode.N,
        KeyCode.O,
        KeyCode.P,
        KeyCode.Q,
        KeyCode.R,
        KeyCode.S,
        KeyCode.T,
        KeyCode.U,
        KeyCode.V,
        KeyCode.W,
        KeyCode.X,
        KeyCode.Y,
        KeyCode.Z,

        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
    };

    private void Start()
    {
        if (!gameText) gameText = GameObject.Find("GameText").GetComponent<Text>();
        if (!flow) flow = GetComponent<DialogueFlow>();
    }

    #region Input type change methods
    public void inputNone()
    {
        inputNumbers = false;
        inputCharacters = false;
    }

    public void inputNumeric(int max = int.MaxValue)
    {
        inputNumbers = true;
        inputCharacters = false;
        maxInput = max;
    }

    public void inputText()
    {
        inputNumbers = true;
        inputCharacters = true;
    }
    #endregion
    
    
    // Update is called once per frame
    void Update()
    {
        inputCheck();
        timer += Time.deltaTime;
        caretTimer += Time.deltaTime;
        if (timer > timeStep) {
            timer = 0;
            if(charIndex < baseText.Length)
            {
                currentText += baseText[charIndex];
                charIndex++;
                redraw();
            }
        }
        if (caretTimer > caretTimeStep) {
            caretTimer = 0;
            caret = !caret;
            redraw();
        }
    }

    private void inputCheck()
    {
        //se sto ancora scrivendo non lascio la possibilità di scrivere
        if (charIndex < baseText.Length) return;
        #region ALPHANUMERIC INPUT
        if (inputCharacters)
        {
            #region ENTER CHECK
            if (Input.GetKeyDown(KeyCode.Return))
            {
                flow.Flow(input);
            }
            #endregion

            #region KEYS CHECK
            foreach (KeyCode kcode in alphanumeric)
            {
                if (Input.GetKeyDown(kcode))
                {
                    string temp = "";
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        temp = kcode.ToString();
                    }
                    else
                    {
                        temp = kcode.ToString().ToLower();
                    }
                    temp = temp.Replace("Alpha", "").Replace("alpha", "");
                    input += temp;
                    redraw();
                }
            }
            #endregion
        }
        #endregion

        #region ONLY NUMERIC INPUT
        else if (inputNumbers)
        {
            #region ENTER CHECK
            if (Input.GetKeyDown(KeyCode.Return))
            {
                int useless;
                if (int.TryParse(input, out useless))
                {
                    flow.Flow(input);
                }
                else
                {
                    //TODO: BEEEEEEEEEP!!!
                    Debug.Log("BEEEEEP!!!");
                }
            }
            #endregion

            #region INPUT NUMBER
            for (int i = 0; i < numbers.Length; i++)
            {
                if (Input.GetKeyDown(numbers[i]))
                {
                    input += i;
                    if (int.Parse(input) > maxInput || input == "0")
                    {
                        input = input.Substring(0, input.Length - 1);
                        //TODO: BEEEEEEEEEP!!!
                        Debug.Log("BEEEEEP!!!");
                    }
                    redraw();
                }
            }
            #endregion

        }
        #endregion

        #region NO INPUT, JUST ENTER
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                flow.Flow(input);
            }
        }
        #endregion

        #region GENERIC BACKSPACE
        if (Input.GetKeyDown(KeyCode.Backspace) && input.Length > 0)
        {
            if (input.Length > 0)
                input = input.Substring(0, input.Length - 1);
            redraw();
        }
        #endregion
    }

    void redraw() {
        gameText.text = currentText + input;
        if (caret) gameText.text += "_";
    }

    public void setBaseText(string text)
    {
        input = "";
        charIndex = 0;
        baseText = text;
        currentText = "";
        redraw();
    }

    public void appendBaseText(string text) {
        baseText += input + "\r\n" + text;
        charIndex += input.Length;
        input = "";
        redraw();
    }

    private string replace_variables(string text)
    {
        Regex regex = new Regex(@"<\?\w*\?>");

        foreach (Match match in regex.Matches(text))
        {
            text = text.Replace(match.Value, (string)flow.dialogue.getVariable(match.Value.Replace("<?", "").Replace("?>", "")));
        }

        return text;
    }
}
