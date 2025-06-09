extends Control

@onready var info_panel = $"Margin/Info Panel"
#@onready var icon_button = $"Margin/ScrollContainer/Entry List/TextureButton"
@onready var sprite = $"Margin/Info Panel/Sprite"
@onready var name_label = $"Margin/Info Panel/Name"
@onready var description_label = $"Margin/Info Panel/Description"
@onready var stats_label = $"Margin/Info Panel/Stats"
@onready var entry_list = $"Margin/ScrollContainer/Entry List"

signal return_to_main_menu

func _ready():
	info_panel.visible = false
	_populate_icons()
	

func _populate_icons():
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


func _on_icon_pressed(entry):
	sprite.texture = load(entry["sprite_in_game"])
	name_label.text = entry["name"]
	description_label.text = entry["description"]
	stats_label.text = entry["stats"]
	info_panel.visible = true


func _on_back_to_main_pressed() -> void:
	emit_signal("return_to_main_menu")
	queue_free()
