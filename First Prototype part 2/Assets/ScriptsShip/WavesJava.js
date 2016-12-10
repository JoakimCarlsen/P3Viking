var speed = 1.0;
var Height = 0.2;
private var baseHeight : Vector3[];
var useOriginal : boolean = false;

public var heightX = 1.0;
//public var heightY = 1;
//public var heightZ = 1;

function Update () {
    var mesh : Mesh = GetComponent(MeshFilter).mesh;
	
    if (baseHeight == null)
        baseHeight = mesh.vertices;
	
    // 
	
    var vertices = new Vector3[baseHeight.Length];
    for (var i=0;i<vertices.Length;i++)
    {
        var vertex = baseHeight[i];

      
       // baseHeight[i].x = baseHeight[i].x * heightX;
       // baseHeight[i].y = baseHeight[i].y * heightY;
       // baseHeight[i].z = baseHeight[i].z * heightZ;

        if (useOriginal) {
            vertex.y += Mathf.Sin((Time.time * speed+ baseHeight[i].x  + baseHeight[i].y + baseHeight[i].z)/ heightX) * Height;
        } else {
            vertex.y += Mathf.Sin(Time.time * speed+ baseHeight[i].x + baseHeight[i].y) * (Height*.5) + Mathf.Sin(Time.time * speed+ baseHeight[i].z + baseHeight[i].y) * (Height*.5);
        }
		
        vertices[i] = vertex;
    }
    mesh.vertices = vertices;
    mesh.RecalculateNormals();
	
    /*gameObject.Destroy(GetComponent(MeshCollider));
	
    var collider : MeshCollider = GetComponent(MeshCollider);
    if (collider == null) {
        collider = gameObject.AddComponent(MeshCollider);
        collider.isTrigger = true;
    }*/
	
}