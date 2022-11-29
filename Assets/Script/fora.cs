using UnityEngine;

public class fora : MonoBehaviour
{
    [SerializeField]
    private float _forward;
    [SerializeField]
    private float _back;
    [SerializeField]
    private float _right;
    [SerializeField]
    private float _left;
    
    void FixedUpdate()
    {
        gameObject.transform.position += Vector3.right/_right;
        gameObject.transform.position += Vector3.forward/_forward;
        gameObject.transform.position += Vector3.left/_left;
        gameObject.transform.position += Vector3.back/_back;
    }
}
