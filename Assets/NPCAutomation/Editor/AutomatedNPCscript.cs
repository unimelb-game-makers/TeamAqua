using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
/* 
- be an inspector tool, click FIX NPC COLLIDERS
- via npcModelScript_FP, find all NPCmodels prefab
- adjust their colliders, trig collider include Player Layer, non-trig exclude Player Layer



 */

namespace AutomateNPC {
    public class AutomatedNpc : MonoBehaviour {

        public static void HandleColliderLayer() { 
            LayerMask playerLayer = LayerMask.GetMask("Player");
            string npcModelScript_FP = $"Assets/Prefabs/Models/NPCs"; // Path to NPC models folder

            string[] modelInfos = AssetDatabase.FindAssets("t:Prefab", new[] { npcModelScript_FP});

            foreach (string guid in modelInfos)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                GameObject npcModelObj = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                Debug.Log($"found file in models dir: {npcModelObj.name} of type: {npcModelObj.GetType()}");
                foreach (Collider collider in  npcModelObj.GetComponents<Collider>())
                {
                    if (collider.isTrigger) // if trigger, include Player layer
                    {
                        if ((collider.includeLayers & playerLayer) == 0)
                        {
                            collider.includeLayers = playerLayer;
                            Debug.Log($"Included Player layer to trigger collider on {npcModelObj.name}");
                        }  
                    }
                    else // if non-trigger, exclude Player layer
                    {
                        if ((collider.excludeLayers & playerLayer) == 0)
                        {
                            collider.excludeLayers = playerLayer;
                            Debug.Log($"Excluded Player layer to nontrig collider on {npcModelObj.name}");
                        } 
                    }
                }            
            }
        }
    }
}