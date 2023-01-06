using System.Collections;
using UnityEngine;

public class ExplosionControll: MonoBehaviour {

    [SerializeField] private string label;
    private GameObject obj;

    void Start() {
        obj = GameObject.Find(label);
    }

    void Update() {
        StartCoroutine(DestroyObj());
    }

    // Corrotina
    IEnumerator DestroyObj() {
        // Espera um determinado tempo até executar o codigo abaixo.
        yield return new WaitForSeconds(0.5f);
        Destroy(obj.gameObject);
        Destroy(gameObject);
    }
}
