using UnityEngine;

public class ReloadBox : MonoBehaviour
{
    public void ConsumingAmmo(float reloadSpeed)
    {
        if (transform.localScale.x < 0.5f)
        {
            Destroy(gameObject);
        }
        transform.localScale -= new Vector3(1f, 1f, 1f) * (Time.deltaTime * reloadSpeed);
    }
}
