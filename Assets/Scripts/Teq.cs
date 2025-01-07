using System;
using UnityEngine;
using UnityEngine.UI;

public class Teq : MonoBehaviour
{
    [SerializeField] Button Button;
    public enum TestEnum
    {
        Test1,
        Test2,
        Test3
    }

    public void TestFilter(TestEnum test)
    {
        Debug.Log(test);
    }

    void Start()
    {
        Debug.Log("Start");
        int size = TestEnum.GetValues(typeof(TestEnum)).Length;
        Debug.Log(size);
        for (int i = 0; i < size; i++)
        {
            Button.onClick.AddListener(() => TestFilter((TestEnum)0));
        }
    }
}
