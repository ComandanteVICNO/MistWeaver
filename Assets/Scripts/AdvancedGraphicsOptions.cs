using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AdvancedGraphicsOptions : MonoBehaviour
{
    [SerializeField]  public UniversalRenderPipelineAsset urpAsset;

    public TMP_Dropdown aaDropdown;
    public TMP_Dropdown textureDropdown;
     

    public Slider renderValue;
    public TMP_Text renderValueText;

    void Start()
    {
        aaDropdown.onValueChanged.AddListener(delegate
        {
            OnAAChanged(aaDropdown);
        });

        textureDropdown.onValueChanged.AddListener(delegate
        {
            OnTextureDropdownChanged(textureDropdown);
        });

        
        SetAADropdown();
        SetRenderScaleSlider();

        SetTextureDropdown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void SetAADropdown()
    {
        aaDropdown.value = urpAsset.msaaSampleCount;
    }

    public void OnAAChanged(TMP_Dropdown aaDropdownValue)
    {
        switch (aaDropdownValue.value)
        {
            case 0:
                urpAsset.msaaSampleCount = 1; // No MSAA
                break;
            case 1:
                urpAsset.msaaSampleCount = 2; // 2x MSAA
                break;
            case 2:
                urpAsset.msaaSampleCount = 4; // 4x MSAA
                break;
            case 3:
                urpAsset.msaaSampleCount = 8; // 8x MSAA
                break;

        }
    }

    public void SetTextureDropdown()
    {
        textureDropdown.value = QualitySettings.globalTextureMipmapLimit;
    }

    public void OnTextureDropdownChanged(TMP_Dropdown tDropdown)
    {
        QualitySettings.globalTextureMipmapLimit = tDropdown.value;
    }

    

    public void SetRenderScaleSlider()
    {
        
        renderValue.value = urpAsset.renderScale;
    }

    public void ChangeRenderScale (float scale)
    {
        urpAsset.renderScale = scale;
        float scaleConversion = scale * 100.0f;
        renderValueText.text = $"{scaleConversion:F0}%";
    }



}
