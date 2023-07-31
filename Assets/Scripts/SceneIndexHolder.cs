using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneIndexHolder
{
    private static int initialSceneBuildIndex;

    public static int InitialSceneBuildIndex
    {
        get { return initialSceneBuildIndex; }
        set { initialSceneBuildIndex = value; }
    }
}

