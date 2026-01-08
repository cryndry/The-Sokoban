using UnityEngine;

public class BlockGenerator : LazySingleton<BlockGenerator>
{
    [SerializeField] private Transform blocksParent;

    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private GameObject playerPrefab;


    public Block GenerateBlock(BlockType type, Vector3 position)
    {
        LevelTheme activeLevelTheme = ThemeManager.Instance.ActiveLevelTheme;

        GameObject floor = Instantiate(floorPrefab, position, Quaternion.identity, blocksParent);
        floor.GetComponentInChildren<SpriteRenderer>().sprite = activeLevelTheme.floorSprite;

        switch (type)
        {
            case BlockType.TARGET:
            case BlockType.BOX_ON_TARGET:
            case BlockType.PLAYER_ON_TARGET:
                GameObject target = Instantiate(targetPrefab, position, Quaternion.identity, blocksParent);
                target.GetComponentInChildren<SpriteRenderer>().sprite = activeLevelTheme.targetSprite;
                break;
            default:
                break;
        }

        Block block = null;
        switch (type)
        {
            case BlockType.BOX:
                {
                    GameObject blockGO = Instantiate(boxPrefab, position, Quaternion.identity, blocksParent);
                    block = blockGO.GetComponent<Box>();
                    (block as Box).IsOnTarget = false;
                    break;
                }
            case BlockType.BOX_ON_TARGET:
                {
                    GameObject blockGO = Instantiate(boxPrefab, position, Quaternion.identity, blocksParent);
                    block = blockGO.GetComponent<Box>();
                    (block as Box).IsOnTarget = true;
                    break;
                }
            case BlockType.WALL:
                {
                    GameObject blockGO = Instantiate(wallPrefab, position, Quaternion.identity, blocksParent);
                    block = blockGO.GetComponent<Wall>();
                    break;
                }
            case BlockType.PLAYER:
                {
                    GameObject blockGO = Instantiate(playerPrefab, position, Quaternion.identity, blocksParent);
                    block = blockGO.GetComponent<Player>();
                    break;
                }
            default:
                break;
        }

        return block;
    }
}