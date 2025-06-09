extends Control

@onready var info_panel = $"Margin/Info Panel"
@onready var sprite = $"Margin/Info Panel/Sprite"
@onready var name_label = $"Margin/Info Panel/Name"
@onready var description_label = $"Margin/Info Panel/Description"
@onready var stats_label = $"Margin/Info Panel/Stats"
@onready var entry_list = $"Margin/ScrollContainer/Entry List"

@onready var toggle_tower_button = $"Margin/Toggle Buttons/Toggle Tower"
@onready var toggle_enemy_button = $"Margin/Toggle Buttons/Toggle Enemy"

var current_mode := "tower"

signal return_to_main_menu

func _ready():
	info_panel.visible = false

	toggle_tower_button.button_pressed = true
	toggle_enemy_button.button_pressed = false
	_populate_tower_icons()

	toggle_tower_button.pressed.connect(_on_toggle_tower_pressed)
	toggle_enemy_button.pressed.connect(_on_toggle_enemy_pressed)


func _populate_tower_icons():
	entry_list.get_children().map(func(child): child.queue_free())
	
	for key in GameData["tower_data"]:
		var entry = GameData["tower_data"][key]
		var icon_button = TextureButton.new()
		icon_button.ignore_texture_size = true
		icon_button.custom_minimum_size = Vector2(150, 150)
		icon_button.stretch_mode = icon_button.STRETCH_SCALE
		icon_button.texture_normal = load(entry["sprite_icon"])
		icon_button.pressed.connect(_on_icon_pressed.bind(entry))
		entry_list.add_child(icon_button)


func _populate_enemy_icons():
	entry_list.get_children().map(func(child): child.queue_free())
	
	for key in GameData["enemy_data"]:
		var entry = GameData["enemy_data"][key]
		var icon_button = TextureButton.new()
		icon_button.ignore_texture_size = true
		icon_button.custom_minimum_size = Vector2(150, 150)
		icon_button.stretch_mode = icon_button.STRETCH_SCALE
		icon_button.texture_normal = load(entry["sprite_icon"])
		icon_button.pressed.connect(_on_icon_pressed.bind(entry))
		entry_list.add_child(icon_button)


func _on_icon_pressed(entry):
	sprite.texture = load(entry["sprite_in_game"])
	name_label.text = entry["name"]
	description_label.text = entry["description"]
	stats_label.text = entry["stats"]
	info_panel.visible = true


func _on_toggle_tower_pressed():
	if current_mode == "tower":
		toggle_tower_button.button_pressed = true
		return
	current_mode = "tower"
	toggle_tower_button.button_pressed = true
	toggle_enemy_button.button_pressed = false
	info_panel.visible = false
	_populate_tower_icons()


func _on_toggle_enemy_pressed():
	if current_mode == "enemy":
		toggle_enemy_button.button_pressed = true
		return
	current_mode = "enemy"
	toggle_enemy_button.button_pressed = true
	toggle_tower_button.button_pressed = false
	info_panel.visible = false
	_populate_enemy_icons()


func _on_back_to_main_pressed() -> void:
	emit_signal("return_to_main_menu")
	queue_free()


func _on_close_panel_pressed() -> void:
	info_panel.visible = false
