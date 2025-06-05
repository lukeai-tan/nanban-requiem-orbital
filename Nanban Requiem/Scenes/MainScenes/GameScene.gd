extends Node2D

@onready var tower_builder = preload("res://Scenes/MainScenes/TowerBuilder.gd").new()
@onready var wave_spawner = preload("res://Scenes/MainScenes/WaveSpawner.gd").new()

signal game_finished(result)

var ui
var base_health := 5.0

func _ready():
	ui = get_node("UI")
	add_child(tower_builder)
	add_child(wave_spawner)

	var map_node = get_node("Map1")
	tower_builder.map_node = map_node
	tower_builder.ui = ui
	ui.tower_builder = tower_builder
	wave_spawner.map_node = map_node
	
	wave_spawner.wave_complete.connect(_on_wave_complete)

	for i in get_tree().get_nodes_in_group("tower_options"):
		i.pressed.connect(tower_builder.initiate_build_mode.bind(i.name))

	wave_spawner.start_next_wave()

func _on_wave_complete():
	wave_spawner.start_next_wave()
	
func on_base_damage(damage: float) -> void:
	base_health -= damage
	ui.update_health_bar(base_health)

	if base_health <= 0:
		game_finished.emit("game_finished")


func _process(delta):
	if tower_builder.build_mode:
		tower_builder.update_tower_preview()
	wave_spawner._process(delta)


## Left Click to Deploy, Right Click to Cancel
func _unhandled_input(event):
	if event.is_action_released("ui_cancel") and tower_builder.build_mode:
		tower_builder.cancel_build_mode()
		
	if event.is_action_released("ui_accept") and tower_builder.build_mode:
		tower_builder.verify_and_build()
		tower_builder.cancel_build_mode()
