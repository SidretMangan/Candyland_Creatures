using UnityEngine;

abstract public class BaseScriptableObject : ScriptableObject
{
    virtual public void load()
    {
        // Override at the subclass.
    }

    virtual public void save()
    {
        // Override at the subclass.
    }

    virtual public void reset()
    {
        // Override at the subclass.
    }
    // Indexer Syntax
    public object this[string fieldName]
    {
        get { return GetType().GetField(fieldName).GetValue(this); }
        set { GetType().GetField(fieldName).SetValue(this, value); }
    }
}
