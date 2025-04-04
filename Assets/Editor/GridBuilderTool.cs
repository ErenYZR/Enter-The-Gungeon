using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class GridBuilderTool
{
	private static GameObject selectedPrefab;
	private static float gridSize = 1f;
	private static int rotationIndex = 0; // 0, 90, 180, 270

	static GridBuilderTool()
	{
		SceneView.duringSceneGui += OnSceneGUI;
	}

	private static void OnSceneGUI(SceneView sceneView)
	{
		// Mouse pozisyonunu al
		Event e = Event.current;

		if (selectedPrefab == null) return;

		Handles.BeginGUI();

		if (selectedPrefab.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
		{
			Sprite sprite = spriteRenderer.sprite;
			if (sprite != null)
			{
				Texture2D texture = sprite.texture;

				Vector2 mousePos = Event.current.mousePosition;
				Vector2 size = (sprite.bounds.size) * (100f);

				Rect rect = new Rect(mousePos - size / 2, size);

				GUI.color = new Color(1, 1, 1, 0.5f);
				GUI.DrawTexture(rect, texture);
				GUI.color = Color.white;
			}
		}

		Handles.EndGUI();


		// Mouse pozisyonunu world koordinatýna çevir
		Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
		Vector3 worldPos = ray.origin;
		RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

		if (hit.collider != null)
		{
			Vector3 pos = hit.point;
			Vector3 gridPos = new Vector3(
				Mathf.Floor(pos.x / gridSize) * gridSize + gridSize / 2f,
				Mathf.Floor(pos.y / gridSize) * gridSize + gridSize / 2f,
				0);

			Handles.color = Color.cyan;
			Handles.DrawWireCube(gridPos, Vector3.one);

			// Týklama kontrolü
			if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
			{
				PlacePrefab(gridPos);
				e.Use(); // Olayý tüket
			}

			// R ile döndürme
			if (e.type == EventType.KeyDown && e.keyCode == KeyCode.R)
			{
				rotationIndex = (rotationIndex + 1) % 4;
				e.Use();
			}
		}
	}

	private static void PlacePrefab(Vector3 position)
	{
		Quaternion rotation = Quaternion.Euler(0, 0, rotationIndex * 90);
		GameObject placed = (GameObject)PrefabUtility.InstantiatePrefab(selectedPrefab);
		placed.transform.position = position;
		placed.transform.rotation = rotation;
		Undo.RegisterCreatedObjectUndo(placed, "Place Prefab");
	}

	[MenuItem("Tools/Grid Builder/Select Prefab")]
	private static void SelectPrefab()
	{
		selectedPrefab = Selection.activeGameObject;
		Debug.Log($"Prefab seçildi: {selectedPrefab?.name}");
	}

	[MenuItem("Tools/Grid Builder/Deselect Prefab")]
	public static void DeselectPrefab()
	{
		selectedPrefab = null;
		SceneView.RepaintAll();
		Debug.Log("Prefab seçimi temizlendi.");
	}

}
