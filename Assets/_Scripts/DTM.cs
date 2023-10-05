using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTM : MonoBehaviour
{
    public static DTM Instance;
    private void Awake()
    {
        Instance = this;
    }
    [HideInInspector]
    public HashSet<string> states;
    [HideInInspector]
    public HashSet<char> inputAlphabet;
    [HideInInspector]
    public HashSet<char> tapeAlphabet;
    [HideInInspector]
    public Dictionary<Tuple<string, char>, Tuple<string, char, char>> transitions;
    [HideInInspector]
    public string startState;
    [HideInInspector]
    public string acceptState;
    [HideInInspector]
    public string rejectState;
    [HideInInspector]
    public int headPosition;
    [HideInInspector]
    public List<char> tape;

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
            print($" head position : {headPosition}");
            print($" symbole : {writeSymbol}");
            CommandPattern.Instance.commands.Add(new ChangeCaseCommand(headPosition, writeSymbol.ToString()));

            if (moveDirection == 'R')
            {
                bool createR = false;
                headPosition++;
                if (headPosition == tape.Count)
                {
                    createR = true;
                    tape.Add('B');
                }
                CommandPattern.Instance.commands.Add(new MoveCommand("R", createR, headPosition));
            }
            else if (moveDirection == 'L')
            {
                bool createL = false;
                headPosition--;
                if (headPosition < 0)
                {
                    createL = true;
                    tape.Insert(0, 'B');
                    headPosition = 0;
                }
                CommandPattern.Instance.commands.Add(new MoveCommand("L", createL, headPosition));
            }

            currentState = newState;
            CommandPattern.Instance.commands.Add(new ChangeStateCommand(newState));
        }
        string tapeFinal = new String(tape.ToArray());
        // Provide values for the Result struct fields
        return new Result(currentState, tapeFinal);
    }

}
