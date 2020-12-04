using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    

    List<Tower> towers = new List<Tower>();
    List<Tower> newTowers = new List<Tower>();

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        //foreach (Tower tower in towers)
        //{
        //    tower.Update();
        //}
    }

    public void AddTower(Tower tower)
    {
        tower.Create();
        newTowers.Add(tower);
    }
}
