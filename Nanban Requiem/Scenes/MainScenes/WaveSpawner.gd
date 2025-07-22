extends Node2D

var current_wave = 0
var enemies_in_wave = 0
var all_enemies_in_wave_spawned = false
var map_to_load: String = "Map1"

signal wave_complete

var map_node: Node2D

func set_map(map):
	map_node = map

func _process(_delta):
	if map_node:
		var path = map_node.get_node_or_null("Path2D")
		if all_enemies_in_wave_spawned and path.get_child_count() == 0:
			all_enemies_in_wave_spawned = false
			wave_complete.emit()


func start_next_wave():
	var wave_data = retrieve_wave_data()
	await(get_tree().create_timer(0.2)).timeout
	spawn_enemies(wave_data, map_node.get_node("Path2D"))


func retrieve_wave_data():
	var wave_data = GameData.wave_data[map_to_load]
	current_wave += 1
	enemies_in_wave = wave_data.size()
	return wave_data


func spawn_enemies(wave_data, path):
	for i in wave_data:
		await get_tree().create_timer(i[1]).timeout

		var enemy_scene = load("res://Scenes/Enemies/" + i[0] + ".tscn")
		var enemy = enemy_scene.instantiate();
		enemy.call("Initialize", path);
		enemy.connect("DamageBase", Callable(get_parent(), "on_base_damage"))
	all_enemies_in_wave_spawned = true
