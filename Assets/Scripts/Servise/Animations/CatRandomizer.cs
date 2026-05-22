using System.Collections.Generic;
using UnityEngine;

public class CatRandomizer : MonoBehaviour
{
    [SerializeField] private List<Animator> _controller = new List<Animator>(3);

    private bool _isNeedRandomize = true;
    private float _passTime = 0;

    private int _currentControllerIntex = 0;

    private void FixedUpdate()
    {
        _passTime -= Time.fixedDeltaTime;

        if (_passTime <= 0)
            _isNeedRandomize = true;

        if (_isNeedRandomize)
        {
            _controller[_currentControllerIntex].gameObject.SetActive(false);
            _currentControllerIntex = Random.Range(1, 3);
            _controller[_currentControllerIntex].gameObject.SetActive(true);
            _passTime = _controller[_currentControllerIntex].GetCurrentAnimatorStateInfo(0).length;
            _isNeedRandomize = false;

            Debug.Log(_passTime);
        }
    }
}