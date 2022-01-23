using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*   
 *   - 무당집 대문에 기둥 부분 레이어 분리를 실수로 까먹으셔서,
 *   - 문을 지나갈 때만 플레이어의 레이어를 잠깐 변경하는 식으로 구현 (이렇게 안 하면, 대문 기둥보다 화면 앞 쪽으로 넘어감)
 *   - 나무, 무당집 대문, 오른쪽 문, 왼쪽 문 모두 Order in Layer = 0
 *   - Sorting Layer를 통해 레이어 렌더링 순서 부여함
 */


public class GateWallLayerController : MonoBehaviour
{

    private string switChingLayerName = "HiddenArea";
    private Dictionary<GameObject, string> dic = new Dictionary<GameObject, string>();


    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject targetObject = collision.gameObject;
        SpriteRenderer[] renderers = collision.gameObject.GetComponentsInChildren<SpriteRenderer>();

        if (renderers != null && collision.gameObject != null)
        {
            string originName = renderers.Length > 0 ? renderers[0].sortingLayerName : null;  // 좀 더 좋은 방법은 없을까? 가비지 많이 나올 것 같은데..
            if (originName != null)
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    renderer.sortingLayerName = switChingLayerName;
                }
                dic.Add(targetObject, originName);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject targetObject = collision.gameObject;
        SpriteRenderer[] renderers = collision.gameObject.GetComponentsInChildren<SpriteRenderer>();

        if (renderers != null && targetObject != null)
        {
            if (dic.ContainsKey(targetObject))
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    renderer.sortingLayerName = dic[targetObject];
                }
                dic.Remove(targetObject);
            }
        }
    }
}
