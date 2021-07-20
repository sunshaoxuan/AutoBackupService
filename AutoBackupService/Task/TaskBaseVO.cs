using System;

public abstract class TaskBaseVO
{
    public String TaskName { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime ModifiedTime { get; set; }

    public TaskBaseVO()
	{
	}

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public abstract void InitTaskKeyValue(string key, string value);
}
