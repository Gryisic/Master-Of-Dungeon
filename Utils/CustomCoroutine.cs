using System;
using System.Collections;
using UnityEngine;

public abstract class CustomCoroutineBase 
{
    protected Coroutine _coroutine;
    protected MonoBehaviour _host;

    protected abstract event Action _finished;

    protected bool _isProcess => _coroutine != null;
}

public class CustomCoroutine : CustomCoroutineBase
{
    protected override event Action _finished;

    private Func<IEnumerator> _routine;

    public CustomCoroutine(MonoBehaviour host, Func<IEnumerator> routine, Action finished)
    {
        _host = host;
        _routine = routine;
        _finished = finished;
    }

    public CustomCoroutine(MonoBehaviour host, Func<IEnumerator> routine) : this(host, routine, null) { }

    public void Start()
    {
        Stop();

        _coroutine = _host.StartCoroutine(Process());
    }

    public void Stop()
    {
        if (_isProcess)
        {
            _host.StopCoroutine(_coroutine);

            _coroutine = null;
        }
    }

    private IEnumerator Process()
    {
        yield return _routine.Invoke();

        _coroutine = null;

        _finished?.Invoke();
    }
}

public class CustomCoroutine<T> : CustomCoroutineBase 
{
    protected override event Action _finished;

    private Func<T, IEnumerator> _routine;

    public CustomCoroutine(MonoBehaviour host, Func<T, IEnumerator> routine, Action finished) 
    {
        _host = host;
        _routine = routine;
        _finished = finished;
    }

    public CustomCoroutine(MonoBehaviour host, Func<T, IEnumerator> routine) : this(host, routine, null) { }

    public void Start(T argument)
    {
        Stop();

        _coroutine = _host.StartCoroutine(Process(argument));
    }

    public void Stop()
    {
        if (_isProcess)
        {
            _host.StopCoroutine(_coroutine);

            _coroutine = null;
        }
    }

    private IEnumerator Process(T argument)
    {
        yield return _routine.Invoke(argument);

        _coroutine = null;

        _finished?.Invoke();
    }
}
