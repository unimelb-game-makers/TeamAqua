using System.Collections.Generic;

[System.Serializable]
public class Quest
{
    public List<QuestData> quests = new List<QuestData>();
}

[System.Serializable]
public class QuestData
{
    public int id;
    public string title;
    public string description;
    public List<QuestStep> quest_steps = new List<QuestStep>();
    public Reward reward;
    public int current_step_number;
    public bool active;
    public bool finished;
    
}


[System.Serializable]
public class QuestStep
{
    public int step;
    public string description;
    public string objective;
    public string objective_type;
    public int quest_item_id;
    public int quest_item_amount;
    public string location;
    public bool active;
    public bool finished;
}

[System.Serializable]
public class Reward
{
    public int exp;
    public int gold;
    public QuestItem item;
}

[System.Serializable]
public class QuestItem
{
    public int id;
    public int amount;
}