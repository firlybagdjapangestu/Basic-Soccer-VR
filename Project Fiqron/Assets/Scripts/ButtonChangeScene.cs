using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeScene : BaseButtonVR
{
    public string sceneName; // The name of the scene to load
    private Renderer _myRenderer;
    [SerializeField] private Material InactiveMaterial;
    [SerializeField] private Material GazedAtMaterial;

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
        ChangeScene();
        base.OnGazeTrigger();
    }
    void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            Debug.Log("Changing to scene: " + sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty!");
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
    