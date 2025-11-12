using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PiratesDatabaseSO : ScriptableObject
{
    public List<PirateData> piratesData;
}

[Serializable]
public class PirateData
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
}

