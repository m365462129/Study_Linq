using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig
{
    public int TypeID
    {
        get;
        set;
    }

    public int PigID
    {
        get;
        set;
    }

    private string name;
    //private int age;

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public int Age
    {
        get;
        set;
    }

    public override string ToString()
    {
        return " PigID=" + PigID + ", name=" + this.Name + ", age=" + this.Age;
    }
}


public class SmallPig : Pig
{
    public string Name
    {
        get;
        set;
    }

    public int Age
    {
        get;
        set;
    }
}

public class PigType
{
    public int TypeID
    {
        get;
        set;
    }

    public string TypeName
    {
        get;
        set;
    }
}
