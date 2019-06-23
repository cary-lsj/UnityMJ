using System;
using System.Collections;
using System.Collections.Generic;

public class GameBase
{
    static List<WeakReference> mObject = new List<WeakReference>();

    private List<IEnumerator> mRoutineKeys = new List<IEnumerator>();
    private Dictionary<IEnumerator, Stack<object>> mRoutines = new Dictionary<IEnumerator, Stack<object>>();

    public GameBase()
    {
        mObject.Add(new WeakReference(this));
    }

    public static void UpdateAll()
    {
        for (int i = 0; i < mObject.Count; i++)
        {
            if (mObject[i].Target != null)
            {
                GameBase gb = mObject[i].Target as GameBase;
                if (gb != null)
                {
                    gb.TickUpdate();
                }
            }
        }
        for (int i = 0; i < mObject.Count; i++)
        {
            if (mObject[i].Target == null)
            {
                mObject.RemoveAt(i);
            }
        }
    }

    public static void Destroy(GameBase gb)
    {
        for (int i = 0; i < mObject.Count; i++)
        {
            if (mObject[i].Target == gb)
            {
                mObject[i].Target = null;
                return;
            }
        }
    }

    protected void StartCoroutine(IEnumerator routine)
    {
        Stack<object> routineStack = new Stack<object>();
        routineStack.Push(routine);
        mRoutineKeys.Add(routine);
        mRoutines.Add(routine, routineStack);
    }

    protected void StopAllCoroutines()
    {
        foreach (var routine in mRoutines.Values)
        {
            RestRoutine(routine);
        }
        mRoutines.Clear();
        mRoutineKeys.Clear();
    }

    protected void StopCoroutine(IEnumerator routine)
    {
        if (mRoutines.ContainsKey(routine))
        {
            RestRoutine(mRoutines[routine]);
            mRoutines.Remove(routine);
            mRoutineKeys.Remove(routine);
        }
    }

    void RestRoutine(Stack<object> routines)
    {
        foreach (object  o in routines)
        {
            IEnumerator ienumator = o as IEnumerator;
            if (ienumator != null)
            {
                try
                {
                    ienumator.Reset();
                }
                catch
                { }
            }
        }
        routines.Clear();
    }

    private void UpdateCoroutine()
    {
        int count = mRoutineKeys.Count;
        for (int i = count - 1; i >= 0; i--)
        {
            IEnumerator routine = mRoutineKeys[i];
            Stack<object> routineStack = mRoutines[routine];
            if (!DealCoroutine(routineStack))
            {
                if (routineStack.Count > 0)
                {
                    routineStack.Pop();
                }
            }
            if (mRoutines.Count == 0)
            {
                return;
            }
            if (routineStack.Count == 0)
            {
                mRoutineKeys.Remove(routine);
                mRoutines.Remove(routine);
            }
        }
    }

    private bool DealCoroutine(Stack<object> routineStack)
    {
        object routine = routineStack.Peek();
        IEnumerator ienumator = routine as IEnumerator;
        if (ienumator == null)
        {
            return false;
        }
        else
        {
            if (ienumator.MoveNext())
            {
                if (ienumator.Current != null)
                {
                    routineStack.Push(ienumator.Current);
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void TickUpdate()
    {
        UpdateCoroutine();
    }
}
