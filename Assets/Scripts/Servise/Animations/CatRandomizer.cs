using UnityEngine;

public class CatRandomizer : MonoBehaviour
{
    private const float Delay = 2f;

    [SerializeField] private Animator _controller;
    [SerializeField] private string _parametrName;

    private bool _isNeedRandomize = false;
    private float _passTime;

    private void FixedUpdate()
    {
        if (Time.time < _passTime)
            return;

        if (_controller.GetCurrentAnimatorClipInfoCount(0) == 0)
        {
            _isNeedRandomize = true;
            _passTime = Time.time + Delay;
        }

        if (_isNeedRandomize)
        {
            _controller.SetInteger(_parametrName, Random.Range(1,4));
            _isNeedRandomize = false;
        }
    }
}