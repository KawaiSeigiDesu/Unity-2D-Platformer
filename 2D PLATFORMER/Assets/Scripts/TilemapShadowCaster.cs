using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class TilemapShadowCaster : MonoBehaviour {
    private Tilemap tilemap;

    private void Awake() {
        tilemap = GetComponent<Tilemap>();
    }

    private void Start() {
        GenerateShadows();
    }

    public void GenerateShadows() {
        // Clear previous ShadowCasters
        foreach (Transform child in transform) {
            if (child.GetComponent<ShadowCaster2D>() != null) {
                DestroyImmediate(child.gameObject);
            }
        }

        // Generate new ShadowCasters for each tile
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++) {
            for (int y = bounds.yMin; y < bounds.yMax; y++) {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(position)) {
                    GameObject shadowCasterObj = new GameObject($"ShadowCaster_{x}_{y}");
                    shadowCasterObj.transform.parent = transform;
                    shadowCasterObj.transform.position = tilemap.GetCellCenterWorld(position);

                    ShadowCaster2D shadowCaster = shadowCasterObj.AddComponent<ShadowCaster2D>();

                    // Make sure the shadow is rendered
                    shadowCaster.useRendererSilhouette = false;
                    shadowCaster.selfShadows = true;
                }
            }
        }
    }
}
