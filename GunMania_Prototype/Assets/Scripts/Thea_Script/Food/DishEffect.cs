using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishEffect : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public float damage;
    public float recover;

}

[CreateAssetMenu(menuName = "Items/RoyalHotpot")]
public class RoyalHotpot : DishEffect
{
    public float RoyalHotpotDMG()
    {
        return damage;
    }
}

[CreateAssetMenu(menuName = "Items/Hassun")]
public class Hassun : DishEffect
{
    public float HassunHeal()
    {
        return recover;
    }
}

[CreateAssetMenu(menuName = "Items/BuddhaJumpsOverTheWall")]
public class BuddhaJumpsOverTheWall : DishEffect
{
    public float BuddhaJumpsOverTheWallDMG()
    {
        return damage;
    }
}

[CreateAssetMenu(menuName = "Items/FoxtailMilletCake")]
public class FoxtailMilletCake : DishEffect
{
    public float FoxtailMilletCakeDMG()
    {
        return damage;
    }
}
