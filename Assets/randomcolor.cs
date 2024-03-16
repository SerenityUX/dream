using UnityEngine;

public class RandomMaterialPicker : MonoBehaviour
{
    public Material[] materials;

    void Start()
    {
        if (materials.Length > 0)
        {
            GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
        }
    }
}
