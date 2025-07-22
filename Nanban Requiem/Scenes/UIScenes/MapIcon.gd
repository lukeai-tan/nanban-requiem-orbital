@tool
extends Control

@export var map_index: int = 1
@export var map_name: String = "Map 1"
#@export var map_selector_packed: PackedScene = preload("res://Scenes/UIScenes/MapSelector.tscn")
#@onready var map_selector_scene: MapSelector = map_selector_packed.instantiate()
#@export_file("*.tscn") var next_scene_path: String


func _ready() -> void:
	# $Label.text = "Map " + str(map_index)
	$Label.text = map_name


func _process(_delta: float) -> void:
	if Engine.is_editor_hint():
		# $Label.text = "Map " + str(map_index)
		$Label.text = map_name
