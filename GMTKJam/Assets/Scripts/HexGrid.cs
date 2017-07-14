using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

    public int Width = 6;
    public int Height = 6;

    public HexCell CellPrefab;

    public Text CellLabelPrefab;

    Canvas gridCanvas;

    HexCell[] cells;

    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        cells = new HexCell[Height * Width];

        for (int z = 0, i = 0; z < Height;  z++)
        {
            for (int x = 0; x < Width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5F - z / 2) * (HexMetrics.InnerRadius * 2F);
        position.y = 0F;
        position.z = z * (HexMetrics.OuterRadius * 1.5F);

        HexCell cell = cells[i] = Instantiate<HexCell>(CellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;

        Text label = Instantiate<Text>(CellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.z);
        label.text = x.ToString() + "\n" + z.ToString();
    }
}
