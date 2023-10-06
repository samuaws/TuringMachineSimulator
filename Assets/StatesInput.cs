using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatesInput : MonoBehaviour
{
    public GameObject holder;
    public List<GameObject> statesInputs = new List<GameObject>();
    public GameObject InputPrefab;
    HashSet<string> finalState = new HashSet<string>();
    public GameObject nextScreen;

    public void AddState()
    {
        statesInputs.Add(Instantiate(InputPrefab,holder.transform));
    }
    public void Suivant()
    {
        foreach (GameObject go in statesInputs) 
        {
            finalState.Add(go.GetComponent<TMP_InputField>().text);
            print(go.GetComponent<TMP_InputField>().text);
        }
        DTM.Instance.states = finalState;
        gameObject.SetActive(false);
        nextScreen.SetActive(true);
    }
}
