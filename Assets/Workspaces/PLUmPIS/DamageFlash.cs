using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{

    [SerializeField] private Color _flashColor = Color.red;
    [SerializeField] private float _flashTime = 0.25f;

    private SkinnedMeshRenderer[] _meshRenderers;
    private Material[] _materials;

    private Coroutine _damageFlashCoroutine;

    private void Awake()
    {
        _meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        Init();
    }

    private void Init()
    {
        _materials = new Material[_meshRenderers.Length];

        //assign sprite renderer materials to _materials
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _materials[i] = _meshRenderers[i].material;
        }
    }

    public void CallDamageFlash()
    {
        Debug.Log("Damage Flash called");
        _damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        //set the color
        SetFlashColor();
        SetFlashAmount(1f);

        //lerp the flash amount
        //float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while(elapsedTime < _flashTime)
        {
            //iterate elapsedTime
            elapsedTime += Time.deltaTime;

            //lerp the flash amount
            float currentFlashAmount = Mathf.Lerp(1f, 0f, (elapsedTime/_flashTime));
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor()
    {
        //set the color
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetColor("_FlashColor", _flashColor);
        }
    }

    private void SetFlashAmount(float amount)
    {
        //set the flash amount
        for (int i = 0; i <_materials.Length; i++)
        {
            _materials[i].SetFloat("_FlashAmount", amount);
        }
    }
}
