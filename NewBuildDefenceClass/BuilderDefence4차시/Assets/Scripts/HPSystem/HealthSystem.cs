using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event Action OnDamaged;
    public event Action OnDied;

    [SerializeField] private int _hpMax = 100;
    private int _hpCur;

    private void Awake()
    {
        _hpCur = _hpMax;
    }

    public bool IsFullHealth()
    {
        return _hpCur == _hpMax;
    }

    public int GetHealthAmount()
    {
        return _hpCur;
    }

    public float GetHealthAmountNomalized()
    {
        return (float)_hpCur / _hpMax;
    }

    public void SetHealthAmountMax(int hpMax, bool updateHpAmount)
    {
        _hpMax = hpMax;
        if(updateHpAmount) _hpCur = _hpMax;
    }

    public void Damage(int damageAmount)
    {
        _hpCur = Mathf.Clamp(_hpCur - damageAmount, 0, _hpMax);
        OnDamaged?.Invoke();

        if(IsDead()) OnDied.Invoke();
    }

    public bool IsDead()
    {
        return _hpCur <= 0;
    }
}
