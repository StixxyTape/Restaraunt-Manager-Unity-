using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Handles general Customer Spawning, aswell as their order and stuff. Will update with chatgpt later.

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _customerPrefab; // Reference to the customer prefab

    private void Awake()
    {
        StartCoroutine(SpawnCustomers());
    }

    public IEnumerator SpawnCustomers()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f));

            Instantiate(_customerPrefab, transform.position, Quaternion.identity);

            gameObject.transform.position += new Vector3(1f, 0, 0);
        }
        
    }

}
