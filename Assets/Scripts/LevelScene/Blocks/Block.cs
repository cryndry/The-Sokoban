using UnityEngine;

public abstract class Block : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer sr;

    public abstract GameObjectTypes BlockType { get; }
}