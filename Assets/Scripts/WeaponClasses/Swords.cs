using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using inputData = System.Tuple<UnityEngine.Vector2, UnityEngine.Vector3, bool, bool, bool, bool>;

public class Swords : WeaponBehaviour
{
    // Start is called before the first frame update
    (Vector2, float) Behave( inputData FrameInputs, inputData LastFrameInputs)
    {
    	return (Vector2.zero, 0);
    }
}
