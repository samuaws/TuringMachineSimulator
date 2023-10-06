using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public TMP_Dropdown ifState;
    public TMP_Dropdown ifSymbole;
    public TMP_Dropdown thenState;
    public TMP_Dropdown thenSymbole;
    public TMP_Dropdown direction;
    // Start is called before the first frame update
    void Start()
    {
        foreach(string op in DTM.Instance.states)
        {
            ifState.options.Add(new TMP_Dropdown.OptionData() { text = op });
            thenState.options.Add(new TMP_Dropdown.OptionData() { text = op });
        }
        foreach(char sym in DTM.Instance.tapeAlphabet)
        {
            ifSymbole.options.Add(new TMP_Dropdown.OptionData() { text = sym.ToString() });
            thenSymbole.options.Add(new TMP_Dropdown.OptionData() { text = sym.ToString() });
            
        }
        direction.options.Add(new TMP_Dropdown.OptionData() { text = "R"});
        direction.options.Add(new TMP_Dropdown.OptionData() { text = "L"});
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
