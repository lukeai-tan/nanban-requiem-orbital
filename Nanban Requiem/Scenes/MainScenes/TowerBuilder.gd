extends Node2D

var map_node: Node2D
var build_mode: bool = false
var build_valid: bool = false
var build_tile
var build_location: Vector2
var build_type: String
var ui
var tower_manager: Node2D
var tower_preview_node = null
var dp_bar: Control

var tower_exclusions : TileMapLayer
var low_ground : TileMapLayer
var high_ground : TileMapLayer

func initiate_build_mode(tower_type):
	# Base case where build mode is initiated during build mode
	if build_mode:
		cancel_build_mode()

	Engine.set_time_scale(0.3)

	build_type = tower_type
	build_mode = true
	ui.set_tower_preview(build_type, get_global_mouse_position())

func update_tower_preview():
	var mouse_position = get_global_mouse_position()
	
	# check if current tile is a tower exclusion
	var exclusion_tile = tower_exclusions.local_to_map(mouse_position)
	var tile_position = tower_exclusions.map_to_local(exclusion_tile)
	if tower_exclusions.get_cell_source_id(exclusion_tile) != -1:
		ui.update_tower_preview(tile_position, "f00")
		build_valid = false
		return

	# check if current tile has an existing tower
	for t in map_node.get_node("Towers").get_children():
		if t.position == tile_position:
			ui.update_tower_preview(tile_position, "f00")
			build_valid = false
			return
	
	# check if current tile is a low tile
	var low_tile = low_ground.local_to_map(mouse_position)
	var is_low_tile : bool = low_ground.get_cell_source_id(low_tile) != -1

	# check if current tile is a high tile
	var high_tile = high_ground.local_to_map(mouse_position)
	var is_high_tile : bool = high_ground.get_cell_source_id(high_tile) != -1
	
	var valid
	if build_type.begins_with("RangedTower"):
		valid = is_high_tile
	elif build_type.begins_with("MeleeTower") or build_type.begins_with("Obstacle"):
		valid = is_low_tile
		
	if valid:
		ui.update_tower_preview(tile_position, "fff")
		build_valid = true
		build_location = tile_position
		build_tile = exclusion_tile
	else:
		ui.update_tower_preview(tile_position, "f00")
		build_valid = false
		

func cancel_build_mode():
	build_mode = false
	build_valid = false
	ui.get_node("TowerPreview").free()
	Engine.set_time_scale(GameData.time_scale)

# IF build location is valid, instantiate selected tower scene
# position it at stored tile
func verify_and_build():
	if build_valid and tower_manager and tower_manager.can_place_tower():
		if(!dp_bar.can_spend_dp(5)):
			print("Insufficient DP to deploy tower!")
			return
		var new_tower = load("res://Scenes/Towers/" + build_type + ".tscn").instantiate()
		new_tower.position = build_location
		map_node.get_node("Towers").add_child(new_tower)
		
		build_mode = false
		build_valid = false
		
		Engine.set_time_scale(GameData.time_scale)
