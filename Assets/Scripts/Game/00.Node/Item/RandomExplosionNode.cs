using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomExplosionNode : OneSwapItemNode
{
    [SerializeField]
    private int countExplosionNode;

    public override void ExplosionNode()
    {
        base.ExplosionNode();
    }


    public override void Swap()
    {
        base.Swap();
    }


    public override List<Coordinate> ItemDetect(Nodes nodes = null)
    {
        List<Coordinate> coordinates = new List<Coordinate>();

        List<Node> nodeList = nodes.GetNodes();
   
        List<Node> copy_nodeList = new List<Node>();
    
          
        foreach(var n in nodeList)
        {
            if(n != null && !(n is WallNode))
            {
                copy_nodeList.Add(n);
            }
        }

        for(int i = 0; i < countExplosionNode; ++i)
        {

           if(nodeList.Count == 0)
            {
                break;
            }

           Node node;
           int index;

           (node,index) = RandomManager.RandomDrawWithIndex(copy_nodeList);

            copy_nodeList.RemoveAt(index);

            Coordinate coordinate = new Coordinate();
            coordinate.x = node.X;
            coordinate.y = node.Y;

            coordinates.Add(coordinate);
        }



        return coordinates;
    }


}
