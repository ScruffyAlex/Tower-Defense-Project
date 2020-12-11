using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int Money { get; set; } = 20;

    [SerializeField]
    private GameObject tower1;
    [SerializeField]
    private GameObject tower2;
    [SerializeField]
    private GameObject tower3;
    [SerializeField]
    private GameObject textBox;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q) && Money >= 5) {
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //snap position
            float rx = Mathf.Floor(mousePos.x) + .5f;
            float ry = Mathf.Floor(mousePos.y) + .5f;

            Vector3 snapPos = new Vector3(rx, ry, -2);

            foreach (GameObject g in towers)
            {
                if (g.transform.position == snapPos) return;
            }

            GameObject newTower = Instantiate(tower1);

            newTower.transform.position = new Vector3(snapPos.x, snapPos.y, -2);

            GameObject.Find("TowerManager").GetComponent<TowerManager>().AddTower(newTower.GetComponent<Tower>());

            Money -= 5;

        }
        else if (Input.GetKeyDown(KeyCode.W) && Money >= 8)
        {
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //snap position
            float rx = Mathf.Floor(mousePos.x) + .5f;
            float ry = Mathf.Floor(mousePos.y) + .5f;

            Vector3 snapPos = new Vector3(rx, ry, -2);

            foreach (GameObject g in towers)
            {
                if (g.transform.position == snapPos) return;
            }

            GameObject newTower = Instantiate(tower2);

            newTower.transform.position = new Vector3(snapPos.x, snapPos.y, -2);

            GameObject.Find("TowerManager").GetComponent<TowerManager>().AddTower(newTower.GetComponent<Tower>());

            Money -= 8;
        }
        else if (Input.GetKeyDown(KeyCode.E) && Money >= 10)
        {
            GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //snap position
            float rx = Mathf.Floor(mousePos.x) + .5f;
            float ry = Mathf.Floor(mousePos.y) + .5f;

            Vector3 snapPos = new Vector3(rx, ry, -2);

            foreach (GameObject g in towers)
            {
                if (g.transform.position == snapPos) return;
            }

            GameObject newTower = Instantiate(tower3);

            newTower.transform.position = new Vector3(snapPos.x, snapPos.y, -2);

            GameObject.Find("TowerManager").GetComponent<TowerManager>().AddTower(newTower.GetComponent<Tower>());

            Money -= 10;
        }

        Text moneyText = textBox.GetComponent<Text>();

        moneyText.text = "Money: " + Money;
    }
}
