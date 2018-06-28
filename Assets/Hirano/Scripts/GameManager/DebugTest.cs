using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : SingletonMonoBehaviour<DebugTest>
{
    public void TestLog()
    {
        Debug.Log("シングルトン!!");
    }
}
