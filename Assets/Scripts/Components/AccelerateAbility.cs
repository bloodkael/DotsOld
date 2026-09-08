using DefaultNamespace.Components.Interfaces;
using System.Collections;
using UnityEngine;

public class AccelerateAbility : MonoBehaviour, IAbility
{
    public float accelerateDelay;
    public float accelerateDuration;

    private float _accelerateTime = float.MinValue;
    private bool _isAccelerated = false;

    public bool IsAccelerated
    {
        get { return _isAccelerated; }
        set { _isAccelerated = value; }
    }

    [HideInInspector] public MoveData moveData;
    public void Execute()
    {
        if (Time.time < _accelerateTime + accelerateDelay || _isAccelerated)
        {
            return;
        }
        _accelerateTime = Time.time;
        StartCoroutine(runAcceleration());
    }

    private IEnumerator runAcceleration()
    {
        _isAccelerated = true;
        yield return new WaitForSeconds(accelerateDuration);
        _isAccelerated = false;
        _accelerateTime = Time.time;
    }
}
