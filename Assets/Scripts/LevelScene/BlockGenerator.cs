using UnityEngine;

public class BlockGenerator : LazySingleton<BlockGenerator>
{
    [SerializeField] private Transform blocksParent;

    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject targetPrefab;

    public Block GenerateBlock(GameObjectTypes type, Vector3 position)
    {
        LevelTheme activeLevelTheme = ThemeManager.Instance.ActiveLevelTheme;

        GameObject floor = Instantiate(floorPrefab, position, Quaternion.identity, blocksParent);
        floor.GetComponentInChildren<SpriteRenderer>().sprite = activeLevelTheme.floorSprite;

        switch (type)
        {
            case GameObjectTypes.TARGET:
            case GameObjectTypes.BOX_ON_TARGET:
            case GameObjectTypes.PLAYER_ON_TARGET:
                GameObject target = Instantiate(targetPrefab, position, Quaternion.identity, blocksParent);
                target.GetComponentInChildren<SpriteRenderer>().sprite = activeLevelTheme.targetSprite;
                break;
            default:
                break;
        }

        Block block = null;
        switch (type)
        {
            case GameObjectTypes.BOX:
                {
                    GameObject blockGO = Instantiate(boxPrefab, position, Quaternion.identity, blocksParent);
                    block = blockGO.GetComponent<Box>();
                    (block as Box).IsOnTarget = false;
                    break;
                }
            case GameObjectTypes.BOX_ON_TARGET:
                {
                    GameObject blockGO = Instantiate(boxPrefab, position, Quaternion.identity, blocksParent);
                    block = blockGO.GetComponent<Box>();
                    (block as Box).IsOnTarget = true;
                    break;
                }
            case GameObjectTypes.WALL:
                {
                    GameObject blockGO = Instantiate(wallPrefab, position, Quaternion.identity, blocksParent);
                    block = blockGO.GetComponent<Wall>();
                    break;
                }
            default:
                break;
        }

        return block;
    }
}