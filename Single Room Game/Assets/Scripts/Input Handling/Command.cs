using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Command : MonoBehaviour {

    abstract public void Execute();
	
}

public class FocusView : Command
{
    public override void Execute()
    {
        
    }
}

public class InteractWithObject : Command
{
    public override void Execute()
    {

    }
}

public class DoNothing : Command
{
    public override void Execute()
    {

    }
}