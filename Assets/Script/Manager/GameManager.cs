using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager single;
    void Awake()
    {
        single = this;
        //加载script
    }

    void Update()
    {

    }

}
