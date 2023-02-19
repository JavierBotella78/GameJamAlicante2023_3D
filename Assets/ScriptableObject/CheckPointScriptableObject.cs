using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/checkPoint")]
public class CheckPointScriptableObject : ScriptableObject
{
    [SerializeField] private float rotation;
    public float GetRotation => rotation;
    public void SetRotation(float newRotation) { rotation = newRotation; }
}
