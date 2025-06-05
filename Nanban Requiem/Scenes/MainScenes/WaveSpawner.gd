extends Node2D

var current_wave = 0
var enemies_in_wave = 0
var all_enemies_in_wave_spawned = false

signal wave_complete

var map_node: Node2D

func _process(_delta):
	var path = map_node.get_node("Path2D")
	if all_enemies_in_wave_spawned and path.get_child_count() == 0:
		all_enemies_in_wave_spawned = false
		wave_complete.emit()


func start_next_wave():
	var wave_data = retrieve_wave_data()
	await(get_tree().create_timer(0.2)).timeout
	spawn_enemies(wave_data)


func retrieve_wave_data():
	var wave_data = GameData.wave_data["Map1"]
	current_wave += 1
	enemies_in_wave = wave_data.size()
	return wave_data


func spawn_enemies(wave_data):
	for i in wave_data:
		await get_tree().create_timer(i[1]).timeout

		var enemy_scene = load("res://Scenes/Enemies/" + i[0] + ".tscn")
		var enemy = enemy_scene.instantiate()
		enemy.connect("damage_base", Callable(get_parent(), "on_base_damage"))
		var path_follow = PathFollow2D.new()
		path_follow.progress = 0.0
		path_follow.add_child(enemy)

		map_node.get_node("Path2D").add_child(path_follow)
	all_enemies_in_wave_spawned = true
