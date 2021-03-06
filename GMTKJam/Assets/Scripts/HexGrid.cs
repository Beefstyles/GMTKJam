﻿using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int width = 6;
	public int height = 6;

	public Color defaultColour = Color.white;
    public Color touchedColour = Color.magenta;

	public HexCell cellPrefab;
	public Text cellLabelPrefab;

    public GameObject TestBase;

	HexCell[] cells;

	Canvas gridCanvas;
	HexMesh hexMesh;

    private HexCoordinates touchedCellCoords;
    public HexCoordinates SelectedUnitCoords;

    public Vector3 TouchedCellPositon;

    GameUI gameUI;
    GameController gc;
    public AudioSource attackNoise;
    public AudioSource moveNoise;

	void Awake () {
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();

		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				CreateCell(x, z, i++);
			}
		}
	}

	void Start ()
    {
		hexMesh.Triangulate(cells);
        gameUI = FindObjectOfType<GameUI>();
        gc = FindObjectOfType<GameController>();
    }

	public void ColorCell (Vector3 position, Color color) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		HexCell cell = cells[index];
		cell.colour = color;
		hexMesh.Triangulate(cells);
	}

	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
		cell.colour = defaultColour;

		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(inputRay,out hit))
        {
            TouchCell(hit.point);
        }
    }

    public void TouchCell (Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        touchedCellCoords = coordinates;
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        TouchedCellPositon = cell.transform.position;

        hexMesh.Triangulate(cells);
        HandleMovement();
    }

    public HexCoordinates ReturnHexCoords(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        return coordinates;
    }

    public void HandleMovement()
    {
        if (gc.SelectedObject != null)
        {
            if(gc.SelectedObject.GetComponent<UnitBehaviour>() != null)
            {
                if (gc.SelectedObject.GetComponent<UnitBehaviour>().NumberOfActions > 0)
                {
                    gc.SelectedObject.GetComponent<UnitBehaviour>().FindCellLocation();
                    UnitTypes targetUT;
                    if (Mathf.Abs(touchedCellCoords.X - SelectedUnitCoords.X) <= 1
                    && Mathf.Abs(touchedCellCoords.Y - SelectedUnitCoords.Y) <= 1
                    && Mathf.Abs(touchedCellCoords.Z - SelectedUnitCoords.Z) <= 1)
                    {
                        if (OverallHexCoordsDict.GameDictionary.TryGetValue(touchedCellCoords, out targetUT))
                        {
                            switch (targetUT)
                            {
                                case UnitTypes.Enemy:
                                    if(gc.SelectedObject.GetComponent<ObjectInfo>().ut == UnitTypes.Soldier)
                                    {
                                        gc.StartCoroutine("RefreshUnitArray");
                                        gc.CheckDeathCoords(touchedCellCoords);
                                        StartCoroutine(gameUI.SetMessage("Destroyed enemy"));
                                        gc.SelectedObject.GetComponent<UnitBehaviour>().NumberOfActions--;
                                        gc.NumberOfResources += Random.Range(2, 10);
                                        attackNoise.Play();
                                    }
                                    else
                                    {
                                        StartCoroutine(gameUI.SetMessage("Only soldier can attack"));
                                    }
                                    break;
                                default:
                                    StartCoroutine(gameUI.SetMessage("Cannot move here"));
                                    break;
                            }
                        }
                        else
                        {
                            gc.SelectedObject.transform.position = new Vector3(TouchedCellPositon.x, gc.SelectedObject.transform.position.y, TouchedCellPositon.z);
                            gc.SelectedObject.GetComponent<UnitBehaviour>().FindCellLocation();

                            gc.SelectedObject.GetComponent<UnitBehaviour>().NumberOfActions--;

                        }
                    }
                    else
                    {
                        StartCoroutine(gameUI.SetMessage("Cannot move here"));
                    }
                }
                else
                {
                    StartCoroutine(gameUI.SetMessage("No actions remaining"));
                }

            }
        }
            
    }    
}
