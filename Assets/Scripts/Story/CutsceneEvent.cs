using System.Collections;
using UnityEngine;

public abstract class CutsceneEvent : MonoBehaviour
{
    public abstract IEnumerator Execute();
}
