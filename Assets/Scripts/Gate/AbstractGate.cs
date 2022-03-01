using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractGate : MonoBehaviour
{

    public bool IsOpen { get { return _isOpen; } }
    public AudioSource _audioSource;
    protected float rotateSpeed = 2f;
    protected const float MIN_ANGLE = 0f;
    protected const float MAX_ANGLE = 90f;
    protected const float FLOAT_APPROXIMATLEY = 2f;
    protected const string DEFALUT_TAGNAME = "Village";
    protected bool _isOpen = false;
    protected bool _isOpening = false;
    private WaitForSeconds gatePassWaitTime = new WaitForSeconds(0.5f);
    [SerializeField] private Collider2D gateCollider;


    protected abstract IEnumerator Open();
    public abstract void OnEvent(string tag);




    protected bool _Approximately(float x, float y)
    {
        float compareAbsValue = (x - y) > 0f ? x - y : y - x;

        if (compareAbsValue <= FLOAT_APPROXIMATLEY)
            return true;
        return false;
    }

    protected IEnumerator GatePassDelay()
    {
        yield return gatePassWaitTime;

        if (gateCollider.isTrigger || gateCollider == null)
        {
            /// 문이 잠겨서 못 지나가는 용이 아닌, 상호작용을 위한 콜라이더이므로
            yield break;
        }


        if (!_isOpen)
        {
            gateCollider.gameObject.SetActive(false);
        }
    }
}
