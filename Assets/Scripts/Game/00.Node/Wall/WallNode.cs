using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�׻� ��ǥ�� ����
public class WallNode : Node
{


    public override void MoveNode(TileObject tileObject, DirMove dirMove) {}

    public override bool MovableNode() => false;

    public override bool ExplosionableNode() => false;

    public override void ExplosionNode() { }

    

}
