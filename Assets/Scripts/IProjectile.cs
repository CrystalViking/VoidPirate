using UnityEngine;

public interface IProjectile
{
    public float Damage { get; set; }
    void CollisionBehavior(Collider2D collision);
}