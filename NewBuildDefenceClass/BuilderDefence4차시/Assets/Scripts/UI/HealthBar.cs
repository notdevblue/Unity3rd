using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTrm;

    private void Awake()
    {
        barTrm = transform.Find("bar");
    }

    private void Start()
    {
        healthSystem.OnDamaged += CallHealthSystemDamaged;
        CallHealthSystemDamaged();
    }

    private void CallHealthSystemDamaged()
    {
        UpdateBar();
        UpdateHealthBarVisable();
    }

    private void UpdateBar()
    {


    }

    private void UpdateHealthBarVisable()
    {
        gameObject.SetActive(!healthSystem.IsFullHealth());
        barTrm.localScale = new Vector3(healthSystem.GetHealthAmountNomalized(), 1, 1);
    }
}
