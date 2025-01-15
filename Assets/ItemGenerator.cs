using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField] GameObject PFB_Item;
    enum Rarity
    {
        COMMON,
        RARE,
        LEGENDARY,
        EXOTIC,
    }

    [SerializeField] SerializedDictionary<Rarity, RarityInfo> rarityWeight = new SerializedDictionary<Rarity, RarityInfo>();
    int totalWeight;

    //DEBUG
    int commonCount;
    int rareCount;
    int legendaryCount;
    int exoticCount;
    int totalCount;

    private void Awake()
    {
        foreach (var pair in rarityWeight)
        {
            totalWeight += pair.Value.weight;
        }
        StartCoroutine(GenerateItem());
    }

    private IEnumerator GenerateItem()
    {
        float count = 20;
        while (count > 0) {

            float rdm = Random.Range(0, totalWeight);
            foreach (var pair in rarityWeight)
            {
                if (rdm < pair.Value.weight)
                {
                    var go = Instantiate(PFB_Item, gameObject.transform.position + new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)), Quaternion.identity);
                    var main = go.GetComponent<ParticleSystem>().main;
                    main.startColor = pair.Value.glintColor;
                    totalCount++;

                    switch (pair.Key)
                    {
                        case Rarity.COMMON:
                            commonCount++;
                            break;
                        case Rarity.RARE:
                            rareCount++;
                            break;
                        case Rarity.LEGENDARY:
                            legendaryCount++;
                            break;
                        case Rarity.EXOTIC:
                            exoticCount++;
                            break;
                    }
                    break;
                }

                rdm -= pair.Value.weight;
            }
            count--;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.TextField($"Total count: {totalCount}");
        GUILayout.TextField($"Common count: {commonCount}, ({commonCount / (float)totalCount} | {rarityWeight[Rarity.COMMON].weight/totalWeight}");
        GUILayout.TextField($"Rare count: {rareCount}, ({rareCount / (float)totalCount} | {rarityWeight[Rarity.RARE].weight / totalWeight}");
        GUILayout.TextField($"Legendary count: {legendaryCount}, ({legendaryCount / (float)totalCount} | {rarityWeight[Rarity.LEGENDARY].weight / totalWeight}");
        GUILayout.TextField($"Exotic count: {exoticCount}, ({exoticCount / (float)totalCount} | {rarityWeight[Rarity.EXOTIC].weight / totalWeight}");
        GUILayout.EndVertical();
    }
} 

[Serializable]
public struct RarityInfo
{
    public int weight;
    public Color glintColor;
}
