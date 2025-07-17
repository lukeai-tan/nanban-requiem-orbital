extends Node2D

@onready var tower_builder = preload("res://Scenes/MainScenes/TowerBuilder.gd").new()
@onready var wave_spawner = preload("res://Scenes/MainScenes/WaveSpawner.gd").new()
@onready var tower_manager = preload("res://Scenes/MainScenes/TowerManager.gd").new()
@onready var build_bar = get_node("UI/HUD/BuildBar")
@onready var end_game_screen = get_node("UI/EndGameScreen")

signal game_finished(result)
signal game_won()
signal game_lost()

var ui
var map_to_load: String = "Map1"
var base_health := 5.0

func _ready():
	ui = get_node("UI")
	
	var map_node: Node = null
	var map_path = "res://Scenes/Maps/%s.tscn" % map_to_load
	var map_scene = load(map_path)

	if map_scene:
		map_node = map_scene.instantiate()
		map_node.name = "Map"
		add_child(map_node)
	else:
		push_error("Failed to load map: " + map_path)
		return

	add_child(tower_builder)
	add_child(wave_spawner)
	add_child(tower_manager)

	tower_builder.map_node = map_node
	tower_builder.ui = ui
	tower_builder.tower_manager = tower_manager
	tower_builder.tower_exclusions = map_node.get_node("TowerExclusions")
	tower_builder.low_ground = map_node.get_node("Path")
	tower_builder.high_ground = map_node.get_node("Ground")

	ui.tower_builder = tower_builder
	ui.tower_manager = tower_manager

	var build_bar_manager = build_bar
	build_bar_manager.setup(tower_builder)
	build_bar_manager.tower_builder = tower_builder

	tower_manager.set_ui(ui)
	tower_manager.map_node = map_node
	tower_manager.connect("tower_count_changed", Callable(ui, "update_tower_count"))
	
	wave_spawner.map_node = map_node
	wave_spawner.map_to_load = map_to_load
	wave_spawner.wave_complete.connect(_on_wave_complete)
	wave_spawner.start_next_wave()

	game_won.connect(end_game_screen._on_game_won)
	game_lost.connect(end_game_screen._on_game_lost)

	Engine.set_time_scale(1.0)


func _on_wave_complete():
	game_finished.emit("game_finished")
	#wave_spawner.start_next_wave()
	
func on_base_damage(damage: float) -> void:
	base_health -= damage
	ui.update_health_bar(base_health)

	if base_health <= 0:
		#game_finished.emit("game_finished")
		game_lost.emit()


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
