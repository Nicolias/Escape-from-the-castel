using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Transform))]
public class Table : MonoBehaviour
{
    [SerializeField] private Transform _raw;

    private Collider _collider;
    private Transform _transform;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _transform = transform;
    }

    public IEnumerable<Vector3> GeneratePoints(int count)
    {
        float step = _collider.bounds.size.x / count;
        float spawnCoordinate = step / 2f;

        for (int i = 1; i <= count; i++)
        {
            Vector3 position = _raw.position + new Vector3(spawnCoordinate, 0f);

            yield return position;

            spawnCoordinate += step;
        }
    }
}