using UnityEngine;

public class SetOnFire : MonoBehaviour
{
    public Manager manager;

    public GameObject[] buildings;
    public Fire[] fires;

    public int ran;

    private void Awake()
    {
        ran = Random.Range(0, buildings.Length);
        buildings[ran].SetActive(true);

        fires = buildings[ran].GetComponentsInChildren<Fire>(true);

        Fire[] allFires = FindObjectsOfType<Fire>();

        for (int i = 0; i < allFires.Length; i++)
        {
            allFires[i].transform.parent.gameObject.SetActive(false);
        }

        for (int i = 0; i < fires.Length; i++)
        {
            fires[i].transform.parent.gameObject.SetActive(true);
        }
    }

    public void CheckHowManyIsOnFire()
    {
        fires = buildings[ran].GetComponentsInChildren<Fire>(true);

        if(fires.Length == 0)
        {
            manager.SetWin();
        }
    }
}
