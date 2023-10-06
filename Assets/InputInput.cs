using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputInput : MonoBehaviour
{


    public TMP_InputField inputField;
    
    public void Suivant()
    {

        TuringMachine.instance.inputString = inputField.text;
        gameObject.SetActive(false);
        TuringMachine.instance.StartSimulation();
    }
    private void OnEnable()
    {
        inputField.text = "";
    }
}
