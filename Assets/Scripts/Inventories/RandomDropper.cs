using RPG.Inventories;
using UnityEngine;

public class RandomDropper : ItemDropper
{
    [SerializeField] float scatterDistance = 1f;
    [SerializeField] private InventoryItem[] dropLibrary;
    [SerializeField] private int numberOfDrops = 2;

    const int attempts = 30;

    public void RandomDrop()
    {
        for (int i = 0; i < numberOfDrops; i++)
        {
            var item = dropLibrary[Random.Range(0, dropLibrary.Length)];
            DropItem(item, 1);
        }
    }

    protected override Vector3 GetDropLocation()
    {
        for (int i = 0; i < attempts; i++)
        {
            Vector3 randomPoint = new Vector3(GiveRandomRange(transform.position.x), transform.position.y, GiveRandomRange(transform.position.z));

            var absValue = Mathf.Abs(GiveRandomRange(transform.position.x) - transform.position.x);

            if (absValue > 1)
            {
                return randomPoint;
            }
        }

        return transform.position;
    }

    private float GiveRandomRange(float num)
    {
        return  Random.Range(num - scatterDistance, num + scatterDistance);
    }
}
