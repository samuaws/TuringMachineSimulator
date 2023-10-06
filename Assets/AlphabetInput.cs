using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlphabetInput : MonoBehaviour
{
    public GameObject holder;
    public List<GameObject> alphabetInputs = new List<GameObject>();
    public GameObject InputPrefab;
    HashSet<char> finalAlphabet = new HashSet<char>();
    public GameObject nextScreen;


    public void AddState()
    {
        alphabetInputs.Add(Instantiate(InputPrefab, holder.transform));
    }
    public void Suivant()
    {
        foreach (GameObject go in alphabetInputs)
        {
            finalAlphabet.Add(go.GetComponent<TMP_InputField>().text[0]);
            print(go.GetComponent<TMP_InputField>().text);
        }
        DTM.Instance.inputAlphabet = finalAlphabet;
        gameObject.SetActive(false);
        nextScreen.SetActive(true);
    }
}
