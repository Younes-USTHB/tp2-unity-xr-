using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public Light directionalLight;

    public void ToggleLight()
    {
        if (directionalLight != null)
        {
            directionalLight.enabled = !directionalLight.enabled;
        }
    }
}