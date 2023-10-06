using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransitionInput : MonoBehaviour
{
    public GameObject holder;
    public List<GameObject> transitionInputs = new List<GameObject>();
    public GameObject InputPrefab;
    Dictionary<Tuple<string, char>, Tuple<string, char, char>> finaltranstions = new  Dictionary<Tuple<string, char>, Tuple<string, char, char>>();
    public GameObject nextScreen;

    public void AddState()
    {
        transitionInputs.Add(Instantiate(InputPrefab, holder.transform));
    }
    public void Suivant()
    {
        foreach (GameObject go in transitionInputs)
        {
            var transition = go .GetComponent<Transition>();
            finaltranstions.Add(
            Tuple.Create(transition.ifState.options[transition.ifState.value].text, transition.ifSymbole.options[transition.ifSymbole.value].text[0]), Tuple.Create(transition.thenState.options[transition.thenState.value].text, transition.thenSymbole.options[transition.thenSymbole.value].text[0], transition.direction.options[transition.direction.value].text[0])
                );
        }
        DTM.Instance.transitions = finaltranstions;
        gameObject.SetActive(false);
        nextScreen.SetActive(true);
    }
}
