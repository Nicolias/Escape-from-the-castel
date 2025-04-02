using System.Collections;
using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private Transform _cupContainerTransform;
    [SerializeField] private Transform _cupTransform;
    [SerializeField] private Transform _spawnPointTransform;

    public Transform Transform => _cupContainerTransform;

    public IEnumerator Open()
    {
        float offset = 0.5f;
        float startPosition = _cupTransform.position.y;
        float endPosition = _cupTransform.position.y + offset;

        yield return StartCoroutine(MoveVertical(endPosition));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(MoveVertical(startPosition));
    }

    public IEnumerator InsertRoutine(Transform child)
    {
        child.position = _spawnPointTransform.position;

        yield return Open();

        child.SetParent(Transform);
    }

    private IEnumerator MoveVertical(float targetYPosition)
    {
        while (Mathf.Approximately(_cupTransform.position.y, targetYPosition) == false)
        {
            _cupTransform.position = Vector3.MoveTowards(_cupTransform.position, new Vector3(_cupTransform.position.x, targetYPosition, _cupTransform.position.z), Time.deltaTime);

            yield return null;
        }
    }
}