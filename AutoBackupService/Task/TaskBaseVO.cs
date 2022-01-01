using System;

public abstract class TaskBaseVO
{
    public String TaskName { get; set; }

    public DateTime CreatedTime { get; set; } = DateTime.Now; //默认当前时间

    public DateTime ModifiedTime { get; set; } = DateTime.Now; //默认当前时间

    public Int64 Interval { get; set; } = 3600000; //默认1小时

    public DateTime LastRunTime { get; set; }

    public Int64 LastRunTerm { get; set; }

    public DateTime NextRunTime { get; set; } = DateTime.Now; //默认当前时间

    public int SessionRunTimes { get; set; } = 0; //默认0


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

    public  void InitTaskKeyValue(string key, string value)
    {
        if (key.ToUpper().Equals("TASKNAME"))
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("任务名称 [TaskName] 不能为空");
            }
            TaskName = value;
        }

        if (key.ToUpper().Equals("CREATEDTIME"))
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("任务创建时间 [CreatedTime] 不能为空");
            }
            CreatedTime = DateTime.Parse(value);
        }

        if (key.ToUpper().Equals("MODIFIEDTIME"))
        {
            if (!string.IsNullOrEmpty(value))
            {
                ModifiedTime = DateTime.Parse(value);
            }
        }

        if (key.ToUpper().Equals("INTERVAL"))
        {
            if (!string.IsNullOrEmpty(value))
            {
                Interval = int.Parse(value);
            }
        }
    }
}
