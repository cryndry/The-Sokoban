using UnityEngine;

public abstract class Block : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer sr;

    public abstract BlockType BlockType { get; }
}