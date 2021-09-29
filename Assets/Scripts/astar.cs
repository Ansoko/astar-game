using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astar : MonoBehaviour
{

    void Start()
    {
        float[,,] grille = new float[10, 10, 1]; //distance depuis méchant
        Array.Clear(grille, 0, grille.Length);
        //int.MaxValue représente les obstacles
        grille[3, 0, 0] = int.MaxValue;
        grille[3, 1, 0] = int.MaxValue;
        grille[3, 2, 0] = int.MaxValue;
        grille[3, 3, 0] = int.MaxValue;
        grille[3, 4, 0] = int.MaxValue;
        grille[3, 5, 0] = int.MaxValue;

        grille[1, 8, 0] = int.MaxValue;
        grille[2, 8, 0] = int.MaxValue;
        grille[3, 8, 0] = int.MaxValue;
        grille[4, 8, 0] = int.MaxValue;

        grille[5, 8, 0] = int.MaxValue;
        grille[5, 7, 0] = int.MaxValue;
        grille[5, 6, 0] = int.MaxValue;

        grille[6, 6, 0] = int.MaxValue;
        grille[7, 6, 0] = int.MaxValue;
        grille[8, 6, 0] = int.MaxValue;

        grille[2, 9, 0] = int.MaxValue;

        Vector3 posGentil = new Vector3(2, 2,0);
        Vector3 posMechant = new Vector3(7, 7, 0);
        algoastar(grille, posMechant, posGentil); //méchant vers gentil

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public List<Vector3> algoastar (float[,,] grille, Vector3 d, Vector3 a)
    {
        List<Vector3> listeouverte = new List<Vector3>(); //contient les positions encore à analyser
        List<Vector3> listefermee = new List<Vector3>(); // contient les voisins vérifiés, qui n'ont plus à être analysé
        float[,,] cout = (float[,,])grille.Clone();
        Vector3[,,] predecesseur = new Vector3[10,10,1];

        listeouverte.Add(d);
		while (listeouverte.Count>0)
		{
            //on prend la plus petite valeur dans la liste ouverte
            Vector3 lowest = lowestPrice(listeouverte, cout);
            //on déplace cette valeur dans la liste fermée
            listefermee.Add(lowest);
            listeouverte.Remove(lowest);
            //si cette valeur est la valeur d'arrivée, l'algo est terminé
            if (lowest == a)
            {
                Debug.Log(grille[(int)lowest.x, (int)lowest.y, (int)lowest.z]);
                break;
            }
            //pour toutes les positions adjacentes :
            for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
                    if (lowest.x + i >= grille.GetLength(0)) { continue; }
                    if (lowest.y + j >= grille.GetLength(1)) { continue; }
                    if (lowest.x - i < 0) { continue; }
                    if (lowest.y - j < 0) { continue; }
                    Vector3 temp = new Vector3(lowest.x + i, lowest.y + j, lowest.z);
                    if (!listeouverte.Contains(temp) && !listefermee.Contains(temp) && (grille[(int)lowest.x + i, (int)lowest.y + j, (int)lowest.z] != int.MaxValue))
                    {
                        listeouverte.Add(temp);
                        grille[(int)lowest.x + i, (int)lowest.y + j, (int)lowest.z] = grille[(int)lowest.x, (int)lowest.y, (int)lowest.z] + 1; //nombre de déplacement 
                        cout[(int)lowest.x + i, (int)lowest.y + j, (int)lowest.z] = grille[(int)lowest.x + i, (int)lowest.y + j, (int)lowest.z] + distancevoloiseau(temp, a); //cout jusquà la position
                        predecesseur[(int)lowest.x + i, (int)lowest.y + j, (int)lowest.z] = lowest;
                    }
                }
			}
        }

        //list des sommets du chemin le plus court
        List<Vector3> shortestPath = new List<Vector3>();
        Vector3 current = a;
        while (current != d)
        {
            shortestPath.Add(current);
            current = predecesseur[(int)current.x, (int)current.y, (int)current.z];
        }
        shortestPath.Reverse();
        return shortestPath;
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
            Debug.Log("liste ouverte vide ou pas de cout !!");
			throw;
		}
        
    }

    private float distancevoloiseau(Vector3 v2, Vector3 v1) {
        return (float)Math.Sqrt(((v2.x - v1.x) * (v2.x - v1.x)) + ((v2.y - v1.y) * (v2.y - v1.y)) + ((v2.z - v1.z) * (v2.z - v1.z)));
    }
}
