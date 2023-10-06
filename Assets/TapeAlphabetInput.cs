using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapeAlphabetInput : MonoBehaviour
{
    public GameObject holder;
    public List<GameObject> tapeAlphabetInputs = new List<GameObject>();
    public GameObject InputPrefab;
    HashSet<char> finalTapeAlphabet = new HashSet<char>();
    public GameObject nextScreen;


    public void AddState()
    {
        tapeAlphabetInputs.Add(Instantiate(InputPrefab, holder.transform));
    }
    public void Suivant()
    {
        foreach (GameObject go in tapeAlphabetInputs)
        {
            finalTapeAlphabet.Add(go.GetComponent<TMP_InputField>().text[0]);
            print(go.GetComponent<TMP_InputField>().text);
        }
        DTM.Instance.tapeAlphabet = finalTapeAlphabet;
        gameObject.SetActive(false);
        nextScreen.SetActive(true);
    }
}
