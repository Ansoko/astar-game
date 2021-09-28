using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[,] grille = new int[10, 10];
        Vector3 posGentil = new Vector3(2, 2,0); // x, y, cout
        Vector3 posMechant = new Vector3(7, 7, 0);
        algoastar(grille, posMechant, posGentil);

        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void algoastar (int[,] grille, Vector2 d, Vector3 a)
    {
        Vector3 depart = new Vector3(d.x, d.y, 0);
        Vector3 arrivee = new Vector3(a.x, a.y, 0);
        List<Vector3> listeouverte = new List<Vector3>(); //contient les positions encore à analyser
        List<Vector3> listefermee = new List<Vector3>(); // contient les voisins vérifiés, qui n'ont plus à être analysé
        int obstacle = -1; //la valeur d'un obstacle est -1

        listeouverte.Add(depart);
		while (listeouverte.Count>0)
		{
            //on prend la plus petite valeur dans la liste ouverte
            Vector3 lowest = lowestPrice(listeouverte);
            if (lowest == arrivee)
            {
                break;
            }
            //on déplace cette valeur dans la liste fermée
            listefermee.Add(lowest);
            listeouverte.Remove(lowest);
			//pour toutes les positions adjacentes :
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
                    Vector3 y = grille[lowest.x + i, lowest.y + j]; 
                    if (
				}
			}
			
        }

    }
    private Vector3 lowestPrice(List<Vector3> list) //point de la liste avec le poids le plus faible
    {
        Vector3 min = list[0];
        list.RemoveAt(0);
		foreach (var item in list)
		{
            if (min[2] > item[2])
            {
                min = item;
            }
		}
        
        return min;
    }
}
