using UnityEngine;

[CreateAssetMenu(fileName = "Int_Resseting", menuName = "Base/Int_Resetting")]
public class SO_IntBase_resetting : ScriptableObject
{
    public int Value;

    private void OnEnable() {
        Value = 0;
    }
}