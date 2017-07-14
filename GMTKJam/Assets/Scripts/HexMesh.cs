using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour {

	Mesh hexMesh;
	List<Vector3> vertices;
	List<Color> colours;
	List<int> triangles;

	MeshCollider meshCollider;

	void Awake () {
		GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
		meshCollider = gameObject.AddComponent<MeshCollider>();
		hexMesh.name = "Hex Mesh";
		vertices = new List<Vector3>();
		colours = new List<Color>();
		triangles = new List<int>();
	}

	public void Triangulate (HexCell[] cells) {
		hexMesh.Clear();
		vertices.Clear();
		colours.Clear();
		triangles.Clear();
		for (int i = 0; i < cells.Length; i++) {
			Triangulate(cells[i]);
		}
		hexMesh.vertices = vertices.ToArray();
		hexMesh.colors = colours.ToArray();
		hexMesh.triangles = triangles.ToArray();
		hexMesh.RecalculateNormals();
		meshCollider.sharedMesh = hexMesh;
	}

	void Triangulate (HexCell cell) {
		Vector3 centre = cell.transform.localPosition;
		for (int i = 0; i < 6; i++) {
			AddTriangle(
				centre,
				centre + HexMetrics.corners[i],
				centre + HexMetrics.corners[i + 1]
			);
			AddTriangleColor(cell.colour);
		}
	}

	void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
		int vertexIndex = vertices.Count;
		vertices.Add(v1);
		vertices.Add(v2);
		vertices.Add(v3);
		triangles.Add(vertexIndex);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 2);
	}

	void AddTriangleColor (Color color) {
		colours.Add(color);
		colours.Add(color);
		colours.Add(color);
	}
}