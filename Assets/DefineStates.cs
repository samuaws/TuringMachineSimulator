using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefineStates : MonoBehaviour
{
    public TMP_Dropdown startState;
    public TMP_Dropdown acceptState;
    public TMP_Dropdown rejectState;
    public GameObject nextScreen;

    private void Start()
    {
        foreach (string op in DTM.Instance.states)
        {
            startState.options.Add(new TMP_Dropdown.OptionData() { text = op });
            acceptState.options.Add(new TMP_Dropdown.OptionData() { text = op });
            rejectState.options.Add(new TMP_Dropdown.OptionData() { text = op });
        }
    }
    public void Suivant()   
    {
        DTM.Instance.acceptState = acceptState.options[acceptState.value].text;
        DTM.Instance.rejectState = rejectState.options[rejectState.value].text;
        DTM.Instance.startState = startState.options[startState.value].text;
        gameObject.SetActive(false);
        nextScreen.SetActive(true);
    }
}
