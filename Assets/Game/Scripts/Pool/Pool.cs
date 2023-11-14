using UnityEngine;

[CreateAssetMenu(fileName = "Item Pool", menuName = "Pool/ItemPool", order = 1)]
public class Pool : ScriptableObject
{
    public string tagPool;
    public GameObject prefab;
    public int amount;
    public bool isExpandable;
}