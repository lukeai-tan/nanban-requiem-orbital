extends Node2D

var map_node: Node2D
var build_mode: bool = false
var build_valid: bool = false
var build_tile
var build_location: Vector2
var build_type: String
var ui
var tower_preview_node = null

func initiate_build_mode(tower_type):
	# Base case where build mode is initiated during build mode
	if build_mode:
		cancel_build_mode()
		
	build_type = tower_type
	build_mode = true
	tower_preview_node = ui.set_tower_preview(build_type, get_global_mouse_position())


func update_tower_preview():
	var mouse_position = get_global_mouse_position()
	
	# tile layers that don't allow tower placements
	var tower_exclusions = map_node.get_node("TowerExclusions")
	var path_layer = map_node.get_node("Path")
	
	# check if current tile is from tower exclusion tilemaplayer
	var current_tile = tower_exclusions.local_to_map(mouse_position)
	var tile_position = tower_exclusions.map_to_local(current_tile)
	
	# check if current tile is from path tilemaplayer
	var invalid_by_exclusion : bool = tower_exclusions.get_cell_source_id(current_tile) != -1
	var path_tile = path_layer.local_to_map(mouse_position)
	var is_path_tile : bool = path_layer.get_cell_source_id(path_tile) != -1
	
	# check if current tile has an existing tower
	var tile_is_occupied : bool = false
	for t in map_node.get_node("Towers").get_children():
		if t.position == tile_position:
			tile_is_occupied = true
			break
			
	var valid : bool = false
	
	if build_type.begins_with("RangedTower"):
		valid = not invalid_by_exclusion and not is_path_tile and not tile_is_occupied
	elif build_type.begins_with("MeleeTower"):
		valid = is_path_tile and not tile_is_occupied
		
	if valid:
		ui.update_tower_preview(tile_position, "fff")
		build_valid = true
		build_location = tile_position
		build_tile = current_tile
	else:
		ui.update_tower_preview(tile_position, "f00")
		build_valid = false
		

func cancel_build_mode():
	build_mode = false
	build_valid = false
	ui.get_node("TowerPreview").free()

# IF build location is valid, instantiate selected tower scene
# position it at stored tile
func verify_and_build():
	if build_valid:
		var new_tower = load("res://Scenes/Towers/" + build_type + ".tscn").instantiate()
		new_tower.position = build_location
		new_tower.built = true
		new_tower.build_location = build_location
		map_node.get_node("Towers").add_child(new_tower)
		
		if tower_preview_node and tower_preview_node.is_inside_tree():
			tower_preview_node.queue_free()
			tower_preview_node = null
		
		build_mode = false
		build_valid = false
