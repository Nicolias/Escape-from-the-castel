using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CupsGame : MonoBehaviour
{
    [SerializeField] private Table _table;
    [SerializeField] private Cup _cupPrefab;
    [SerializeField] private int _cupsCount;
    [SerializeField] private Transform _ball;

    private List<Cup> _cups;
    private Shuffler _shuffler = new Shuffler();

    private Coroutine _coroutine;

    private void Start()
    {
        _cups = _table.GeneratePoints(_cupsCount).Select(position => Instantiate(_cupPrefab, position, Quaternion.identity)).ToList();
        _coroutine = StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator StartGame()
    {
        int randomCupIndex = Random.Range(0, _cupsCount);
        Transform ball = Instantiate(_ball);
        Cup randomCup = _cups[randomCupIndex];

        yield return randomCup.StartCoroutine(randomCup.InsertRoutine(ball));

        yield return StartCoroutine(_shuffler.Blend(_cups.Select(cup => cup.Transform).ToList(), this));
    }
}

public class Shuffler
{
    

    public Shuffler()
    {
        
    }

    public IEnumerator Blend(List<Transform> cups, MonoBehaviour owner)
    {
        int minBlendsCount = 5;
        int maxBlendsCount = 10;
        int blendingsCount = Random.Range(minBlendsCount, maxBlendsCount);
        int minCupsIndex = 0;
        int centerCupsIndex = cups.Count / 2;
        Transform joint = new GameObject().GetComponent<Transform>();

        for (int i = 0; i < blendingsCount; i++)
        {
            int firstCupIndex = Random.Range(minCupsIndex, centerCupsIndex + 1);
            int secondCupIndex = Random.Range(centerCupsIndex + 1, cups.Count);
            Transform temporaryCup = cups[firstCupIndex];
            cups[firstCupIndex] = cups[secondCupIndex];
            cups[secondCupIndex] = temporaryCup;

            yield return owner.StartCoroutine(SwitchCupsRoutine(cups[firstCupIndex], cups[secondCupIndex], joint));
        }

        GameObject.Destroy(joint.gameObject);
    }

    private IEnumerator SwitchCupsRoutine(Transform firstCup, Transform secondCup, Transform joint)
    {
        float percentDistance = 0.5f;
        joint.position = Vector3.Lerp(firstCup.position, secondCup.position, percentDistance);

        firstCup.SetParent(joint);
        secondCup.SetParent(joint);

        yield return RotateJoint(joint);
    }

    private IEnumerator RotateJoint(Transform joint)
    {
        float rotationY = 180f;
        Quaternion targetRotation = joint.rotation * Quaternion.Euler(0f, rotationY, 0f);
        float animationTimeFromSeconds = 3f;

        while (joint.rotation != targetRotation)
        {
            joint.rotation = Quaternion.RotateTowards(joint.rotation, targetRotation, rotationY / animationTimeFromSeconds * Time.deltaTime);
            yield return null;
        }

        joint.DetachChildren();
    }
}