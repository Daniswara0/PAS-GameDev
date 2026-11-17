using UnityEngine;

public class Cyclops : Enemy
{
    public bool flag = true;

    public override void Serang()
    {
        Debug.Log("Spike menyerang!");
    }
}