using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public interface ICommand
{
    void Execute();
}
public class MoveCommand : ICommand
{
    public string moveDir;
    public bool create;
    public int nextCase;
    public MoveCommand (string moveDir , bool create,int nextCase) 
    {
        this.moveDir = moveDir;
        this.create = create;
        this.nextCase = nextCase;
    }
    public void Execute()
    {
        if(moveDir == "R")
        {
            TuringMachine.instance.head.transform.Translate(1, 0, 0);
        }
        if(moveDir == "L")
        {
            TuringMachine.instance.head.transform.Translate(-1, 0, 0);
        }
        if (create)
        {
            TuringMachine.instance.GenerateCube(nextCase, 'B');
        }
    }
}
public class ChangeCaseCommand : ICommand
{
    public int tapeCase;
    public string newSymbol;

    public ChangeCaseCommand(int tapeCase, string newSymbol)
    {
        this.tapeCase = tapeCase;
        this.newSymbol = newSymbol;
    }

    public void Execute()
    {
        TuringMachine.instance.tapeCases[tapeCase].GetComponent<TapeCase>().screenText.text = newSymbol;
    }
}
public class ChangeStateCommand : ICommand
{
    public string state;
    public ChangeStateCommand(string state)
    {
        this.state = state;
    }
    public void Execute()
    {
        TuringMachine.instance.head.GetComponent<TapeCase>().screenText.text = state;
    }
}

public class CommandPattern : MonoBehaviour
{
    public static CommandPattern Instance;
    public List<ICommand> commands = new List<ICommand>();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(Test());

    }

    IEnumerator Test() { 
        yield return new WaitForSeconds(.5f);
        if (commands.Count > 0)
        {
            commands[0].Execute();
            commands.RemoveAt(0);
        }
        StartCoroutine(Test());
    }
}
