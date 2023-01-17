using System.Collections;
using UnityEngine;

public class ExplosionControll: MonoBehaviour {

    [SerializeField] private string label;
    private GameObject obj;
    private Vector3 startPosition;

    void Start() {
        obj = GameObject.Find(label);
        startPosition = obj.transform.position;
        StartCoroutine(DestroyAndRecreateBomb());
    }

    // Corrotina
    IEnumerator DestroyAndRecreateBomb() {
        yield return StartCoroutine(DestroyObj());
    }

    IEnumerator DestroyObj() {
        // Espera um determinado tempo até executar o codigo abaixo.
        yield return new WaitForSeconds(0.5f);
        obj.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        obj.transform.position = startPosition;
        obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        obj.SetActive(true);
        Destroy(gameObject);
    }
}
