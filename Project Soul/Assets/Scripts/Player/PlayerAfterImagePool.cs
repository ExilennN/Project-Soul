using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    public static PlayerAfterImagePool Instance; // Статичний екземпляр

    [SerializeField] private GameObject afterImagePrefab; // Префаб післяобразу
    [SerializeField] private int initialPoolSize = 20; // Початковий розмір пулу
    private Queue<GameObject> availableAfterImages = new Queue<GameObject>(); // Черга доступних післяобразів

    private void Awake()
    {
        Instance = this; // Ініціалізація статичного екземпляра
    }

    private void Start()
    {
        // Попереднє завантаження певної кількості післяобразів
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject afterImage = Instantiate(afterImagePrefab);
            afterImage.SetActive(false); // Деактивація після створення
            availableAfterImages.Enqueue(afterImage); // Додавання в чергу
        }
    }

    public GameObject GetFromPool()
    {
        if (availableAfterImages.Count == 0)
        {
            GameObject afterImage = Instantiate(afterImagePrefab);
            afterImage.SetActive(false); // Деактивація після створення
            availableAfterImages.Enqueue(afterImage); // Додавання в чергу
        }

        GameObject obj = availableAfterImages.Dequeue(); // Вилучення з черги
        obj.SetActive(true); // Активація післяобразу
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false); // Деактивація післяобразу
        availableAfterImages.Enqueue(obj); // Повернення в чергу
    }
}
