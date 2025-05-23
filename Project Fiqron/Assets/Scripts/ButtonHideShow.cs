using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHideShow : BaseButtonVR
{
    private Renderer _myRenderer;
    [SerializeField] private Material InactiveMaterial;
    [SerializeField] private Material GazedAtMaterial;

    [SerializeField] private GameObject objectToShow;
    [SerializeField] private GameObject objectToHide;
    protected override void Start()
    {
        base.Start();
        _myRenderer = GetComponent<Renderer>();
    }
    protected override void OnPointerEnter()
    {
        SetMaterial(true);
        base.OnPointerEnter();
    }

    protected override void OnPointerExit()
    {
        SetMaterial(false);
        base.OnPointerExit();
    }

    protected override void OnGazeTrigger()
    {
        ToggleObjectVisibility();
        base.OnGazeTrigger();
    }
    void ToggleObjectVisibility()
    {
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }

        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }

    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
