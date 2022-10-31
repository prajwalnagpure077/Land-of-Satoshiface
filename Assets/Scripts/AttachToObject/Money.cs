using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] int m_money;
    [SerializeField] bool rotate;
    private void Start()
    {
        if (rotate)
        {
            MoneyManager.Instance.toRotateList.Add(transform);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoneyManager.Instance.AddMoney(m_money);
            MoneyManager.Instance.toRotateList.Remove(transform);
            Destroy(gameObject);
        }
    }
}
