using System;
using System.Collections.Generic;
using System.Linq;

public class Pool<TObject>
{
    private readonly Func<TObject> _preloadFunc;
    private readonly Action<TObject> _getAction;
    private readonly Action<TObject> _returnAction;

    public readonly List<TObject> Queue = new();

    public Pool(Func<TObject> preloadFunc, Action<TObject> getAction, Action<TObject> returnAction, int preloadCount)
    {
        _preloadFunc = preloadFunc;
        _getAction = getAction;
        _returnAction = returnAction;

        if (preloadFunc == null)
        {
            return;
        }

        for (int i = 0; i < preloadCount; i++)
        {
            Queue.Add(Return(_preloadFunc()));
        }
    }

    public TObject Get()
    {
        TObject item = GetFirst();

        if (item == null)
        {
            item = _preloadFunc();
            Queue.Add(item);
        }
        _getAction(item);

        return item;
    }

    public TObject Return(TObject item)
    {
        _returnAction(item);
        return item;
    }

    public void ReturnAll()
    {
        Queue.ForEach(item => Return(item));
    }

    public virtual TObject GetFirst()
        => Queue.First();
}
