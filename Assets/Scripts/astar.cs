using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astar : MonoBehaviour
{

    const int inf = int.MaxValue; //infini

    void Start()
    {
        float[,,] grille = new float[10, 10, 1]; //distance depuis méchant
        Array.Clear(grille, 0, grille.Length);
        //-1 représente les obstacles
        grille[2, 3, 0] = inf;
        grille[2, 4, 0] = inf;
        grille[2, 5, 0] = inf;
        grille[2, 6, 0] = inf;
        grille[2, 7, 0] = inf;
        grille[2, 8, 0] = inf;
        grille[5, 4, 0] = inf;
        grille[6, 4, 0] = inf;
        grille[7, 4, 0] = inf;
        grille[8, 4, 0] = inf;
        Vector3 posGentil = new Vector3(2, 2,0); // x, y, cout
        Vector3 posMechant = new Vector3(7, 7, 0);
        algoastar(grille, posMechant, posGentil);

        Debug.Log("test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void algoastar (float[,,] grille, Vector3 d, Vector3 a)
    {

        Vector3 depart = new Vector3(d.x, d.y, 0);
        Vector3 arrivee = new Vector3(a.x, a.y, 0);
        List<Vector3> listeouverte = new List<Vector3>(); //contient les positions encore à analyser
        List<Vector3> listefermee = new List<Vector3>(); // contient les voisins vérifiés, qui n'ont plus à être analysé
        float[,,] cout = grille;

        listeouverte.Add(depart);
		while (listeouverte.Count>0)
		{
            //on prend la plus petite valeur dans la liste ouverte
            Vector3 lowest = lowestPrice(listeouverte, cout);
            Debug.Log(cout[(int)lowest.x, (int)lowest.y, (int)lowest.z]);
            //on déplace cette valeur dans la liste fermée
            listefermee.Add(lowest);
            listeouverte.Remove(lowest);
            //si cette valeur est la valeur d'arrivée, l'algo est terminé
            if (lowest == arrivee)
            {
                break;
            }            //pour toutes les positions adjacentes :
            for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
                    Vector3 temp = new Vector3(lowest.x + i, lowest.y + j, lowest.z);
                    if (!listeouverte.Contains(temp) && !listefermee.Contains(temp) && (grille[(int)lowest.x + i, (int)lowest.y + j, (int)lowest.z] != inf))
                    {
                        listeouverte.Add(temp);
                        grille[(int)lowest.x + i, (int)lowest.y + j, (int)lowest.z]++;
                        cout[(int)lowest.x + i, (int)lowest.y + j, (int)lowest.z] = grille[(int)lowest.x + i, (int)lowest.y + j, (int)lowest.z] + distancevoloiseau(temp, a);
                    }
                }
			}
			
        }
        Debug.Log(cout[(int)arrivee.x, (int)arrivee.y, (int)arrivee.z]);

    }
    private Vector3 lowestPrice(List<Vector3> list, float [,,] grille) //point de la liste avec le poids le plus faible
    {
		try
		{
            Vector3 minvct = list[0];
            list.RemoveAt(0);
            float min = grille[(int)minvct.x, (int)minvct.y, (int)minvct.z];

            foreach (var item in list)
            {
                if (min > grille[(int)item.x, (int)item.y, (int)item.z])
                {
                    min = grille[(int)item.x, (int)item.y, (int)item.z];
                    minvct = item;
                }
            }

            return minvct;
        }
		catch (System.Exception)
		{
            Debug.Log("liste ouverte vide !!");
			throw;
		}
        
    }

    private float distancevoloiseau(Vector3 v1, Vector3 v2) {
        return (float)Math.Sqrt((v2.x - v1.x) * (v2.x - v1.x)) + ((v2.y - v1.y) * (v2.y - v1.y)) + ((v2.z - v1.z) * (v2.z - v1.z));
    }
}
