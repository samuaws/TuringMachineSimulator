using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class Result
{
    public string finalState;
    public string finalTape;


    public Result(string finalState, string tape) 
    {
        this.finalState = finalState;
        this.finalTape = tape;
    }
}
[Serializable]


public class TuringMachine : MonoBehaviour
{
    public static TuringMachine instance;
    public string inputString = "10";
    public GameObject tapeCasePrefab;
    [HideInInspector]
    public GameObject head;
    public Dictionary<int,GameObject> tapeCases = new Dictionary<int,GameObject>();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        var states = new HashSet<string> { "q0", "q1", "q2" };
        var inputAlphabet = new HashSet<char> { '0', '1' };
        var tapeAlphabet = new HashSet<char> { '0', '1', 'B' };
        var transitions = new Dictionary<Tuple<string, char>, Tuple<string, char, char>>
        {
            { Tuple.Create("q0", '0'), Tuple.Create("q0", '1', 'R') },
            { Tuple.Create("q0", '1'), Tuple.Create("q0", '0', 'R') },
            { Tuple.Create("q0", 'B'), Tuple.Create("q1", 'B', 'L') },
        };
        var startState = "q0";
        var acceptState = "q1";
        var rejectState = "q2";


        // Create and run the DTM
        DTM.Instance.states = states;
        DTM.Instance.inputAlphabet = inputAlphabet;
        DTM.Instance.tapeAlphabet = tapeAlphabet;
        DTM.Instance.transitions = transitions;
        DTM.Instance.startState = startState;
        DTM.Instance.acceptState = acceptState;
        DTM.Instance.rejectState = rejectState;


        GenerateInputCubes();
        CreateHead();

        var result = DTM.Instance.Run(inputString);

        if (result.finalState == acceptState)
        {
            print($"{result.finalState} The DTM accepted the input: {inputString}");
            print(result.finalTape);
        }
        else
        {
            print($"{result.finalState} The DTM rejected the input: {inputString}");
            print(result.finalTape);
        }
    }
    void GenerateInputCubes()
    {
        int cubeCounter = 0;
        foreach (char c in inputString)
        {
            tapeCases[cubeCounter] = Instantiate(tapeCasePrefab,Vector3.right * cubeCounter, Quaternion.identity);
            tapeCases[cubeCounter].GetComponent<TapeCase>().screenText.text = inputString[cubeCounter].ToString();
            cubeCounter++;
        }
        tapeCases[cubeCounter] = Instantiate(tapeCasePrefab, Vector3.zero + Vector3.right * cubeCounter, Quaternion.identity);
        tapeCases[cubeCounter].GetComponent<TapeCase>().screenText.text = "B";
    }
    public void GenerateCube(int offset , char symbol)
    {
        tapeCases[offset] = Instantiate(tapeCasePrefab, Vector3.right * offset, Quaternion.identity);
        tapeCases[offset].GetComponent<TapeCase>().screenText.text = symbol.ToString();
    }
    public void CreateHead()
    {
        head = Instantiate(tapeCasePrefab, Vector3.up + Vector3.right * DTM.Instance.headPosition, Quaternion.identity);
        head.GetComponent<TapeCase>().screenText.text = DTM.Instance.startState;
    }
}
