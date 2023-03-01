using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataGraph : MonoBehaviour
{
    // Img & Window 
    public Sprite dataPointSprite;
    private RectTransform graphData;
    public RectTransform labelTemplateX;
    public RectTransform labelTemplateY;
    public RectTransform dashTemplateX;
    public RectTransform dashTemplateY;

    // Data Spacing
    private int offsetX = -400;
    private int offsetY = -100;

    private int axisGridLineCount = 10;

    private float spacingX;
    private float spacingY;

    private float gridSpacingX;
    private float gridSpacingY;

    public DataPoint dataPoint;
    // Data 
    public ListVariable scoresList;
    private List<Vector2> pointPositions = new List<Vector2>();

    private void Awake()
    {
        //labelTemplateX = GameObject.Find("labelTemplateX").GetComponent<RectTransform>();
        //labelTemplateY = GameObject.Find("labelTemplateY").GetComponent<RectTransform>();
    }

    private void Start()
    {

        graphData = GameObject.Find("graphData").GetComponent<RectTransform>();
        //labelTemplateX = GameObject.Find("labelTemplateX").GetComponent<RectTransform>();
        //labelTemplateY = GameObject.Find("labelTemplateY").GetComponent<RectTransform>();

        if(scoresList.listInt.Count == 0)
        {

            // do nothing
           
        }
        
        else
        {

            NormalizeSpacing();

            

            for (int i = 0; i < scoresList.listInt.Count; i++)
            {
                CreateDataPoint(new Vector2(i * spacingX, scoresList.listInt[i] * spacingY));
                pointPositions.Add(new Vector2(i * spacingX, scoresList.listInt[i] * spacingY));

                CreateAxisLabelXY(new Vector2((i - 1) * spacingX, -82f), new Vector2(-1.38f * spacingX, (i - 1) * spacingY), i);
                CreateAxisDashGridXY(new Vector2(i * spacingX, 183f), new Vector2(450f, (i+1) * spacingY), i);
            }

            for (int i=0; i<axisGridLineCount; i++)
            {
                
            }

            ConnectDataWithLines();

        }
    }

    public void VisualizeGraph()
    {
        if (scoresList.listInt.Count == 0)
        {
            //List<GameObject> dataPointObjs = new List<GameObject>; 
            DataPoint[] dataPoints = GameObject.FindObjectsOfType<DataPoint>();

            for (int i =0; i < dataPoints.Length; i++)
            {
                Destroy(dataPoints[i]);
            }
        }

        else
        {

            NormalizeSpacing();



            for (int i = 0; i < scoresList.listInt.Count; i++)
            {
                CreateDataPoint(new Vector2(i * spacingX, scoresList.listInt[i] * spacingY));
                pointPositions.Add(new Vector2(i * spacingX, scoresList.listInt[i] * spacingY));

                CreateAxisLabelXY(new Vector2((i - 1) * spacingX, -82f), new Vector2(-1.38f * spacingX, (i - 1) * spacingY), i);
                CreateAxisDashGridXY(new Vector2(i * spacingX, 183f), new Vector2(450f, (i + 1) * spacingY), i);
            }

            for (int i = 0; i < axisGridLineCount; i++)
            {

            }

            ConnectDataWithLines();

        }
    }

    private void NormalizeSpacing()
    {
        // with 350 pixels of vertical space...
        int maxValue = Mathf.Max(scoresList.listInt.ToArray());
        spacingY = Mathf.Floor(350 / (maxValue));

        // with 900 pixels of horizontal space...
        spacingX = Mathf.Floor(900 / scoresList.listInt.Count);

        gridSpacingX = Mathf.Floor(900 / axisGridLineCount);
        gridSpacingY = Mathf.Floor(350 / maxValue);
    }

    private void CreateDataPoint(Vector2 position)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.AddComponent<DataPoint>();

        gameObject.transform.SetParent(graphData, false);
        gameObject.GetComponent<Image>().sprite = dataPointSprite;
        RectTransform dataRectTransform = gameObject.GetComponent<RectTransform>();
        dataRectTransform.anchoredPosition = position;
        dataRectTransform.sizeDelta = new Vector2(14, 14);
        dataRectTransform.anchorMin = new Vector2(0, 0);
        dataRectTransform.anchorMax = new Vector2(0, 0);

    }

    private void CreateAxisLabelXY(Vector2 positionX, Vector2 positionY, int iterator)
    {
        RectTransform labelX = Instantiate(labelTemplateX);
        labelX.SetParent(graphData);
        labelX.gameObject.SetActive(true);
        labelX.anchoredPosition = new Vector2(positionX.x, positionX.y);
        labelX.GetComponent<TextMeshProUGUI>().text = (iterator + 1).ToString();

        RectTransform labelY = Instantiate(labelTemplateY);
        labelY.SetParent(graphData);
        labelY.gameObject.SetActive(true);
        labelY.anchoredPosition = new Vector2(positionY.x, positionY.y);
        labelY.GetComponent<TextMeshProUGUI>().text = (iterator + 1).ToString();

    }

    private void CreateAxisDashGridXY(Vector2 positionX, Vector2 positionY, int iterator)
    {
        RectTransform dashX = Instantiate(dashTemplateX);
        dashX.SetParent(graphData); 
        dashX.gameObject.SetActive(true);
        dashX.anchoredPosition = new Vector2(positionX.x, positionX.y);

        RectTransform dashY = Instantiate(dashTemplateY);
        dashY.SetParent(graphData);
        dashY.gameObject.SetActive(true);
        dashY.anchoredPosition = new Vector2(positionY.x, positionY.y);
    }

    private void ConnectDataWithLines()
    {
        LineRenderer lr = GameObject.Find("lineBWPoints").GetComponent<LineRenderer>();
        lr.positionCount = scoresList.listInt.Count;

        for (int i = 0; i < scoresList.listInt.Count; i++)
        {
            lr.SetPosition(i, pointPositions[i]);
        }
    }

    public void ClearSavedScores()
    {
        foreach (Transform child in graphData.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
