using UnityEngine;

public abstract class ItemBase 
{
    protected Rigidbody2D rb;

    public void InitializeDefaults(Rigidbody2D rigidbody)
    {
        rb = rigidbody;
    }

    public abstract void Wear(ItemManager itemOnPlayer);
}
