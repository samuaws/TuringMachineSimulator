using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
class DTM
{
    private HashSet<string> states;
    private HashSet<char> inputAlphabet;
    private HashSet<char> tapeAlphabet;
    private Dictionary<Tuple<string, char>, Tuple<string, char, char>> transitions;
    private string startState;
    private string acceptState;
    private string rejectState;
    private int headPosition;
    private List<char> tape;

    public DTM(
        HashSet<string> states,
        HashSet<char> inputAlphabet,
        HashSet<char> tapeAlphabet,
        Dictionary<Tuple<string, char>, Tuple<string, char, char>> transitions,
        string startState,
        string acceptState,
        string rejectState)
    {
        this.states = states;
        this.inputAlphabet = inputAlphabet;
        this.tapeAlphabet = tapeAlphabet;
        this.transitions = transitions;
        this.startState = startState;
        this.acceptState = acceptState;
        this.rejectState = rejectState;
        this.headPosition = 0;
    }

    public Result Run(string inputString)
    {
        //tape.AddRange(inputString);
        this.tape = new List<char>(inputString) { 'B' };
        string currentState = startState;
        foreach (char c in inputString)
        {
            if (!inputAlphabet.Contains(c))
            {
                string tapeEmpty = new String(tape.ToArray());
                return new Result(rejectState, tapeEmpty);
               
            }
        }

        while (currentState != acceptState && currentState != rejectState)
        {
            char currentSymbol = tape[headPosition];

            if (!transitions.TryGetValue(Tuple.Create(currentState, currentSymbol), out var transition))
            {
                currentState = rejectState;
                break;
            }

            string newState = transition.Item1;
            char writeSymbol = transition.Item2;
            char moveDirection = transition.Item3;

            tape[headPosition] = writeSymbol;

            if (moveDirection == 'R')
            {
                headPosition++;
                if (headPosition == tape.Count)
                {
                    tape.Add('B');
                }
            }
            else if (moveDirection == 'L')
            {
                headPosition--;
                if (headPosition < 0)
                {
                    tape.Insert(0, 'B');
                    headPosition = 0;
                }
            }

            currentState = newState;
        }
        string tapeFinal = new String(tape.ToArray());
        // Provide values for the Result struct fields
        return new Result(currentState, tapeFinal);
    }

}


public class TuringMachine : MonoBehaviour
{
    public string inputString = "10";
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
        var dtm = new DTM(states, inputAlphabet, tapeAlphabet, transitions, startState, acceptState, rejectState);
        
        var result = dtm.Run(inputString);

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
}
